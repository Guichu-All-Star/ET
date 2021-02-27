using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UiLoginComponentSystem : AwakeSystem<UILoginComponent>
    {
        public override void Awake(UILoginComponent self)
        {
            self.Awake();
        }
    }

    public class UILoginComponent : Component
    {
        private Text error;
        InputField accountInputField;
        InputField passwordInputField;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            rc.Get<GameObject>("LoginBtn").GetComponent<Button>().onClick.Add(OnLogin);
            rc.Get<GameObject>("RegisterBtn").GetComponent<Button>().onClick.Add(OnRegister);
            GameObject account = rc.Get<GameObject>("Account");
            error = rc.Get<GameObject>("Error").GetComponent<Text>();
            accountInputField = account.GetComponent<InputField>();
            passwordInputField = rc.Get<GameObject>("Password").GetComponent<InputField>();
        }

        public void OnLogin()
        {
            LoginHelper.OnLoginAsync(accountInputField.text, passwordInputField.text).Coroutine();
        }

        public void OnRegister()
        {
            Game.EventSystem.Run(EventIdType.RegisterStart);
        }

        public void SetError(string errorText)
        {
            error.text = errorText;
        }
    }
}
