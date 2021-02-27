using ETModel;
using System.Net;
namespace ETHotfix
{
    [ObjectSystem]
    public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
    {
        public override void Destroy(SessionPlayerComponent self)
        {
            DestroyAsync(self).Coroutine();
        }

        private static async ETVoid DestroyAsync(SessionPlayerComponent self)
        {
            try
            {
                // 发送断线消息
                Log.Info($"111 销毁Player和Session{self.Player.Id}");
                if (self.Player.UnitId != 0)
                {
                    ActorLocationSender actorLocationSender = await Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.Player.UnitId);
                    actorLocationSender.Send(new G2M_SessionDisconnect()).Coroutine();
                }
                Game.Scene.GetComponent<PlayerComponent>()?.Remove(self.Player.Id);

                StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
                //向登录服务器发送玩家下线消息
                IPEndPoint realmIPEndPoint = config.RealmConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmIPEndPoint);
                realmSession.Send(new ETHotfix.G2R_PlayerOffline() { PlayerId = self.Player.Id });

                //服务端主动断开客户端连接
                Game.Scene.GetComponent<NetOuterComponent>().Remove(self.Player.GateSessionID);
                //Log.Info($"将玩家{message.PlayerID}连接断开");

                self.Player.Dispose();
                self.Player = null;
            }
            catch (System.Exception e)
            {
                Log.Trace(e.ToString());
            }
        }
    }
}