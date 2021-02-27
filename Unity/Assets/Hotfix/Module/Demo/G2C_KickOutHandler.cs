using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class G2C_KickOutHandler : AMHandler<G2C_KickOut>
    {
        protected override async ETTask Run(ETModel.Session session, G2C_KickOut message)
        {
			//TODO
            Log.Debug($"G2C_KickOut Reason={message.Reason}");
            await ETTask.CompletedTask;
        }
    }
}