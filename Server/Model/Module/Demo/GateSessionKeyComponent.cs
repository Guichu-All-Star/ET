using System.Collections.Generic;

namespace ETModel
{
    public class GateSessionKeyComponent : Component
    {
        private readonly Dictionary<long, long> sessionKey = new Dictionary<long, long>();

        public void Add(long key, long playerId)
        {
            sessionKey.Add(key, playerId);
            TimeoutRemoveKey(key).Coroutine();
        }

        public long Get(long key)
        {
            long playerId;
            sessionKey.TryGetValue(key, out playerId);
            return playerId;
        }

        public void Remove(long key)
        {
            sessionKey.Remove(key);
        }

        private async ETVoid TimeoutRemoveKey(long key)
        {
            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(20000);
            sessionKey.Remove(key);
        }
    }
}
