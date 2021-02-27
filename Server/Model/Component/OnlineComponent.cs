using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 在线组件，用于记录在线玩家 无扩展
    /// </summary>
    public class OnlineComponent : Component
    {
        /// <summary>
        /// 参数1：永久PlayerID 参数2：对应Player所在的Gate的app编号
        /// </summary>
        private readonly Dictionary<long, int> dictionary = new Dictionary<long, int>();

        /// <summary>
        /// 添加在线玩家 参数1：永久Id 参数2：所在Gate的app编号
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="gateAppId"></param>
        public void Add(long playerId, int gateAppId)
        {
            dictionary.Add(playerId, gateAppId);
        }

        /// <summary>
        /// 获取在线玩家网关服务器ID
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public int GetGateAppId(long playerId)
        {
            int gateAppId;
            dictionary.TryGetValue(playerId, out gateAppId);
            return gateAppId;
        }

        /// <summary>
        /// 移除在线玩家
        /// </summary>
        /// <param name="playerId"></param>
        public void Remove(long playerId)
        {
            if (dictionary.ContainsKey(playerId))
                dictionary.Remove(playerId);
        }
    }
}