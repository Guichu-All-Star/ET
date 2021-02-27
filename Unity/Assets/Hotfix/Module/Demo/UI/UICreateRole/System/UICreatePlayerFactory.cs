using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class UICreatePlayerFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.UICreatePlayer.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UICreatePlayer.StringToAB(), UIType.UICreatePlayer);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UICreatePlayer, gameObject, false);

                ui.AddComponent<UICreatePlayerComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}