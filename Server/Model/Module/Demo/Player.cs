namespace ETModel
{
    public sealed class Player : Entity
    {
        public long UnitId { get; set; }

        /// <summary>
        /// 玩家所在的Gate服务器的AppID
        /// </summary>
        public int GateAppID { get; set; }

        /// <summary>
        /// 玩家所绑定的Seesion.Id 用于给客户端发送消息
        /// </summary>
        public long GateSessionID { get; set; }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
            GateAppID = 0;
            GateSessionID = 0;
        }
    }
}