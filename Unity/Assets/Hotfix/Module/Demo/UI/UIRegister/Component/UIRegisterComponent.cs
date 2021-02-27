using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UiRegisterComponentSystem : AwakeSystem<UIRegisterComponent>
    {
        public override void Awake(UIRegisterComponent self)
        {
            self.Awake();
        }
    }

    public class UIRegisterComponent : Component
    {
        private Text error;
        InputField accountInputField;
        InputField passwordInputField;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            rc.Get<GameObject>("RegisterBtn").GetComponent<Button>().onClick.Add(OnRegister);
            GameObject account = rc.Get<GameObject>("Account");
            error = rc.Get<GameObject>("Error").GetComponent<Text>();
            Log.Info("error" + (error != null));
            accountInputField = account.GetComponent<InputField>();
            passwordInputField = rc.Get<GameObject>("Password").GetComponent<InputField>();
        }

        public void OnRegister()
        {
            LoginHelper.OnRegisterAsync(accountInputField.text, passwordInputField.text).Coroutine();
        }

        public void SetError(string errorText)
        {
            error.text = errorText;
        }
    }
}
