using ETModel;
using System.Net;

namespace ETHotfix
{
    public static class GateHelper
    {
        /// <summary>
        /// 验证Session是否绑定了玩家
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool SignSession(Session session)
        {
            SessionPlayerComponent sessionPlayer = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayer == null || Game.Scene.GetComponent<PlayerComponent>().Get(sessionPlayer.Player.Id) == null)
            {
                return false;
            }
            return true;
        } 
    }
}