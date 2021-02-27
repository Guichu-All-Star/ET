using System;
using ETModel;

namespace ETHotfix
{
    public static class LoginHelper
    {
        public static async ETVoid OnLoginAsync(string account, string password)
        {
            try
            {
                UILoginComponent uiLoginComponent = Game.Scene.GetComponent<UIComponent>().Get(UIType.UILogin).GetComponent<UILoginComponent>();

                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Login r2CLogin = (R2C_Login)await realmSession.Call(new C2R_Login() { Account = account, Password = password });
                realmSession.Dispose();

                if (r2CLogin.Error == ErrorCode.ERR_AccountOrPasswordError)
                {
                    Log.Info("ERR_AccountOrPasswordError");// + login
                    uiLoginComponent.SetError("login error:" + r2CLogin.Error);
                    return;
                }

                // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
                ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;
                gateSession.AddComponent<HeartBeatComponent>();

                // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });

                if (g2CLoginGate.Error == ErrorCode.ERR_ConnectGateKeyError)
                {
                    uiLoginComponent.SetError("login error:" + r2CLogin.Error);
                    // SessionComponent.Instance.Session.Dispose();
                    gateSession.Dispose();
                    return;
                }
                Log.Info("登陆gate成功!");

                // 获取玩家信息
                G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());

                UnityEngine.Debug.Log("g2CPlayerInfo.PlayerInfo" + (g2CPlayerInfo.PlayerInfo != null));
                // 创建Player
                Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                playerComponent.MyPlayer = player;
                if (g2CPlayerInfo.PlayerInfo == null)
                {
                    Game.EventSystem.Run(EventIdType.CreatePlayerStart);
                    return;
                }
                player.Name = g2CPlayerInfo.PlayerInfo.Name;
                player.Gender = g2CPlayerInfo.PlayerInfo.Gender;
                Game.EventSystem.Run(EventIdType.LoginFinish);
                // Game.EventSystem.Run(EventIdType.CreatePlayerFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async ETVoid OnRegisterAsync(string account, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Register r2CRegister = (R2C_Register)await realmSession.Call(new C2R_Register() { Account = account, Password = password });
                session.Dispose();
                realmSession.Dispose();

                if (r2CRegister.Error == ErrorCode.ERR_AccountAlreadyExist)
                {
                    Log.Error("ERR_AccountAlreadyExist");// + login
                    UIRegisterComponent registerComponent = Game.Scene.GetComponent<UIComponent>().Get(UIType.UIRegister).GetComponent<UIRegisterComponent>();
                    registerComponent.SetError("register error:" + r2CRegister.Error);
                    return;
                }
                Game.EventSystem.Run(EventIdType.RegisterFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async ETVoid OnCreatePlayerAsync(string name)
        {
            try
            {
                Player myPlayer = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer;
                G2C_CreatePlayer g2C_CreateUser = (G2C_CreatePlayer)await SessionComponent.Instance.Session.Call(new C2G_CreatePlayer() { Name = name });
                if (g2C_CreateUser.Error != 0)
                {
                    Log.Error("G2C_CreatePlayer Error:" + g2C_CreateUser.Error);// + login
                    return;
                }
                myPlayer.Name = g2C_CreateUser.PlayerInfo.Name;
                myPlayer.Gender = g2C_CreateUser.PlayerInfo.Gender;
                Game.EventSystem.Run(EventIdType.CreatePlayerFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}