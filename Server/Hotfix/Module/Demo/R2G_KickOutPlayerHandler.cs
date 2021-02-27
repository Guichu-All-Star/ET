using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_KickOutPlayerHandler : AMRpcHandler<R2G_KickOutPlayer, G2R_KickOutPlayer>
    {
        protected override async ETTask Run(Session session, R2G_KickOutPlayer request, G2R_KickOutPlayer response, Action reply)
        {
            //获取此PlayerID的网关session
            long sessionId = Game.Scene.GetComponent<PlayerComponent>().Get(request.PlayerId).GateSessionID;
            Session lastSession = Game.Scene.GetComponent<NetOuterComponent>().Get(sessionId);

            //移除session与player的绑定
            lastSession.RemoveComponent<SessionPlayerComponent>();
            reply();
            await ETTask.CompletedTask;
        }
    }
}