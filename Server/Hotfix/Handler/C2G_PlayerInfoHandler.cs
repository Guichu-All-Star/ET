using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    // 用来测试消息包含复杂类型，是否产生gc
    [MessageHandler(AppType.Gate)]
    public class C2G_PlayerInfoHandler : AMRpcHandler<C2G_PlayerInfo, G2C_PlayerInfo>
    {
        protected override async ETTask Run(Session session, C2G_PlayerInfo request, G2C_PlayerInfo response, Action reply)
        {
            //验证Session
            if (!GateHelper.SignSession(session))
            {
                response.Error = ErrorCode.ERR_UserNotOnline;
                reply();
                return;
            }
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
            ETModel.PlayerInfo playerInfo = await dbProxy.Query<ETModel.PlayerInfo>(player.Id);
            if (playerInfo != null)
            {
                response.PlayerInfo = new PlayerInfo { PlayerId = playerInfo.PlayerId, Name = playerInfo.Name, Gender = playerInfo.Gender };
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}
