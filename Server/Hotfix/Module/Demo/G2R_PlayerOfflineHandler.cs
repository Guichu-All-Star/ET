using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOfflineHandler : AMHandler<G2R_PlayerOffline>
    {
        protected override async ETTask Run(Session session, G2R_PlayerOffline message)
        {
            //玩家下线
            Game.Scene.GetComponent<OnlineComponent>().Remove(message.PlayerId);
            await ETTask.CompletedTask;
        }
    }
}