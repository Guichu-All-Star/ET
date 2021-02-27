using ETModel;
using System.Net;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class RealmHelper
    {
        private static int value;
        /// <summary>
        /// 随机生成区号 1~
        /// </summary>
        public static long GenerateId()
        {
            //随机获得GateId 1~2
            int randomGateAppId = RandomHelper.RandomNumber(0, StartConfigComponent.Instance.GateConfigs.Count) + 1;
            long time = TimeHelper.ClientNowSeconds();
            //1540 2822 75   时间为10位数
            //区号取第11位数
            return (randomGateAppId * 100000000000 + time + ++value);
        }

        /// <summary>
        /// 生成指定大区的账号 参数为1以上的整数
        /// </summary>
        /// <param name="GateAppId"></param>
        /// <returns></returns>
        public static long GenerateId(int GateAppId)
        {
            long time = TimeHelper.ClientNowSeconds();
            //1540 2822 75   时间为10位数
            //区号取第11位数
            return (GateAppId * 100000000000 + time + ++value);
        }

        /// <summary>
        /// 查询账号所在大区 参数为1以上的整数
        /// </summary>
        public static int GetGateAppIdFromPlayerId(long userID)
        {
            return (int)(userID / 100000000000);
        }

        /// <summary>
        /// 将已在线的玩家踢下线
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static async Task KickOutPlayer(long playerId)
        {
            //判断用户是否在线，在线则踢下线
            int gateAppId = Game.Scene.GetComponent<OnlineComponent>().GetGateAppId(playerId);
            if (gateAppId != 0) //如果玩家在线 则返回其所在的AppID
            {
                Log.Debug($"玩家{playerId}已在线 将执行踢下线操作");

                //获取此Player所在Gate服务器
                StartConfig userGateConfig = Game.Scene.GetComponent<StartConfigComponent>().Get(gateAppId);
                IPEndPoint userGateIPEndPoint = userGateConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session userGateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(userGateIPEndPoint);

                //获取此Player其它客户端与网关连接session
                /* 这里是向此Player其它客户端发送一条测试消息，                                 */
                /* 你可以在移除它处登录用户前向之前客户端发送一条通知下线的消息让其返回登录界面  */
                Player player = Game.Scene.GetComponent<PlayerComponent>().Get(playerId);

                //向客户端发送玩家下线消息
                Log.Info("它处登录，原登录踢下线《====");
                Session clientSession = Game.Scene.GetComponent<NetOuterComponent>().Get(player.GateSessionID);
                clientSession.Send(new G2C_KickOut() { Reason = 1 });
                //TODO 实现通知客户端下线

                //通知Gate服务器移除指定Player将它处登录用户踢下线
                await userGateSession.Call(new R2G_KickOutPlayer() { PlayerId = playerId });
            }
        }
    }
}