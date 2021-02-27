using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOnlineHandler : AMHandler<G2R_PlayerOnline>
    {
        protected override async ETTask Run(Session session, G2R_PlayerOnline message)
        {
            OnlineComponent onlineComponent = Game.Scene.GetComponent<OnlineComponent>();

            //检查玩家是否在线 如不在线则添加
            if (onlineComponent.GetGateAppId(message.PlayerId) == 0)
            {
                onlineComponent.Add(message.PlayerId, message.GateAppID);
            }
            else
            {
                Log.Error("玩家已在线 Realm服务器收到重复上线请求的异常");
            }
            await ETTask.CompletedTask;
        }
    }
}