using System;
using System.Net;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response, Action reply)
        {
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
            List<ComponentWithId> result = await dbProxy.Query<AccountInfo>($"{{Account:'{request.Account}',Password:'{request.Password}'}}");
            // 随机分配一个Gate
            StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
            string outerAddress = config.GetComponent<OuterConfig>().Address2;
            if (result.Count != 1)
            {
                response.Error = ErrorCode.ERR_AccountOrPasswordError;
                response.Address = outerAddress;
                reply();
                return;
            }
            //Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
            IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
            Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

            AccountInfo accountInfo = (AccountInfo)result[0];
            await RealmHelper.KickOutPlayer(accountInfo.Id);
            // 向gate请求一个key,客户端可以拿着这个key连接gate
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey { PlayerId = accountInfo.Id });
            //Log.Debug("C2R_LoginHandler session.id=" + session.InstanceId);
            //Log.Debug("C2R_LoginHandler gateSession.id = " + gateSession.InstanceId);
            //session.AddComponent<HeartBeatComponent>().CurrentTime = TimeHelper.ClientNowSeconds();


            response.Address = outerAddress;
            response.Key = g2RGetLoginKey.Key;
            reply();
        }
    }
}