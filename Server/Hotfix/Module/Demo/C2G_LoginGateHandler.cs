using System;
using ETModel;
using System.Net;
namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response, Action reply)
        {
            long playerId = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (playerId == 0)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate key验证失败!";
                reply();
                return;
            }
            Player player = Game.Scene.GetComponent<PlayerComponent>().Get(playerId);
            StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
            if (player == null)
            {
                player = ComponentFactory.CreateWithId<Player>(playerId);
                Game.Scene.GetComponent<PlayerComponent>().Add(player);
                player.GateAppID = config.StartConfig.AppId;
                player.GateSessionID = session.InstanceId;
                player.AddComponent<MailBoxComponent>();
            }
            session.AddComponent<SessionPlayerComponent>().Player = player;
            session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);

            response.PlayerId = player.Id;
            reply();

            //构建realmSession通知Realm服务器 玩家已上线
            IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
            Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
            //2个参数 1：UserID 2：GateAppID
            realmSession.Send(new G2R_PlayerOnline() { PlayerId = player.Id, GateAppID = config.StartConfig.AppId });

            session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
            await ETTask.CompletedTask;
        }
    }
}