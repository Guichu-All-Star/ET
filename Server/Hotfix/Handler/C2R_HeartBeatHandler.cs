using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2R_HeartBeatHandler : AMRpcHandler<C2R_HeartBeat, R2C_HeartBeat>
    {
        protected override async ETTask Run(Session session, C2R_HeartBeat message, R2C_HeartBeat response, Action reply)
        {
            //HeartBeatComponent heartBeatComponent = session.GetComponent<HeartBeatComponent>();
            //Log.Debug("C2R_HeartBeatHandler session.id = " + session.InstanceId + ",heartBeatComponent = " + (heartBeatComponent != null));
            //if (heartBeatComponent != null)
            //{
            //    heartBeatComponent.CurrentTime = TimeHelper.ClientNowSeconds();
            //}
            reply();
            await ETTask.CompletedTask;
        }
    }
}