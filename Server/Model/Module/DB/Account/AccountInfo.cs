namespace ETModel
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class AccountInfo : ComponentWithId
    {
        //用户名
        public string Account { get; set; }
        public string Password { get; set; }
    }
}