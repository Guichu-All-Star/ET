namespace ETModel
{
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class PlayerInfo : ComponentWithId
    {
        //唯一用户id
        public long PlayerId { get; set; }
        //昵称
        public string Name { get; set; }
        //性别
        public bool Gender { get; set; }
    }
}