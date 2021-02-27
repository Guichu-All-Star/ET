using System;
using System.Net;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson;
namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_RegisterHandler : AMRpcHandler<C2R_Register, R2C_Register>
    {
        protected override async ETTask Run(Session session, C2R_Register request, R2C_Register response, Action reply)
        {
            Log.Debug("C2R_RegisterHandler session.id=" + session.InstanceId);
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
            List<ComponentWithId> result = await dbProxy.Query<AccountInfo>($"{{Account:'{request.Account}'}}");
            // 随机分配一个Gate
            if (result.Count > 0)
            {
                response.Error = ErrorCode.ERR_AccountAlreadyExist;
                reply();
                return;
            }
            else
            {
                //生成玩家帐号 这里随机生成区号
                AccountInfo newAccount = ComponentFactory.CreateWithId<AccountInfo>(RealmHelper.GenerateId());
                newAccount.Account = request.Account;
                newAccount.Password = request.Password;
                await dbProxy.Save(newAccount);
                response.Account = request.Account;
                response.Password = request.Password;
                reply();
            }
        }
    }
}