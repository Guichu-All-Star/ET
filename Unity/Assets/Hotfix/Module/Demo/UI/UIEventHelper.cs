using System;
using ETModel;

namespace ETHotfix
{
    public static class UIEventHelper
    {
        [Event(EventIdType.CreatePlayerStart)]
        public class CreatePlayerStart_CreateCreatePlayerUI : AEvent
        {
            public override void Run()
            {
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILogin);
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UILogin.StringToAB());
                UI ui = UICreatePlayerFactory.Create();
                Game.Scene.GetComponent<UIComponent>().Add(ui);
            }
        }
        [Event(EventIdType.CreatePlayerFinish)]
        public class CreatePlayerFinish_RemoveCreatePlayerUI : AEvent
        {
            public override void Run()
            {
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.UICreatePlayer);
                // ETModel.Game.Scene.GetComponent<ResourcesComponent>().
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UICreatePlayer.StringToAB());
            }
        }
    }
}