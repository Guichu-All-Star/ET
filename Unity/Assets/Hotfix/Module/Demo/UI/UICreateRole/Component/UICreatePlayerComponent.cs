using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UICreatePlayerComponentSystem : AwakeSystem<UICreatePlayerComponent>
    {
        public override void Awake(UICreatePlayerComponent self)
        {
            self.Awake();
        }
    }

    public class UICreatePlayerComponent : Component
    {
        private Button OKBtn;
        private InputField inputField;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            OKBtn = rc.Get<GameObject>("OKBtn").GetComponent<Button>();
            OKBtn.onClick.Add(this.CreatePlayer);

            inputField = rc.Get<GameObject>("Name").GetComponent<InputField>();
        }

        private void CreatePlayer()
        {
            LoginHelper.OnCreatePlayerAsync(inputField.text).Coroutine();
        }
    }
}
