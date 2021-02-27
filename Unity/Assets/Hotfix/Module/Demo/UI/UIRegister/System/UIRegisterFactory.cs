using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class UIRegisterFactory
    {
        public static UI Create()
        {
	        try
	        {
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(UIType.UIRegister.StringToAB());
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UIRegister.StringToAB(), UIType.UIRegister);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

		        UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UIRegister, gameObject, false);

				ui.AddComponent<UIRegisterComponent>();
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