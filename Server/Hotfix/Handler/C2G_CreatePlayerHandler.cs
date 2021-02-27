using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_CreatePlayerHandler : AMRpcHandler<C2G_CreatePlayer, G2C_CreatePlayer>
    {
        protected override async ETTask Run(Session session, C2G_CreatePlayer request, G2C_CreatePlayer response, Action reply)
        {
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            ETModel.PlayerInfo playerInfo = await dbProxy.Query<ETModel.PlayerInfo>(player.Id);
            if (playerInfo == null)
            {
                playerInfo = ComponentFactory.CreateWithId<ETModel.PlayerInfo>(player.Id);
                playerInfo.PlayerId = player.Id;
                playerInfo.Name = request.Name;
                playerInfo.Gender = request.Gender;
                await dbProxy.Save(playerInfo);
                response.PlayerInfo = new PlayerInfo
                {
                    PlayerId = playerInfo.PlayerId,
                    Name = playerInfo.Name,
                    Gender = playerInfo.Gender
                };
            }
            else
            {
                response.Error = ErrorCode.ERR_PlayerAlreadyCreated;
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}
