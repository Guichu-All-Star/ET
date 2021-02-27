using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2G_ClientLogoutHandler : AMHandler<C2G_ClientLogout>
    {
        protected override async ETTask Run(Session session, C2G_ClientLogout message)
        {
            Log.Info("C2G_ClientLogout");
            await ETTask.CompletedTask;
        }
    }
}