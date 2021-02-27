using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.RegisterFinish)]
	public class RegisterFinish_RemoveRegisterUI: AEvent
	{
		public override void Run()
		{
			Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIRegister);
			ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIRegister.StringToAB());
			UI ui = UILoginFactory.Create();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
		}
	}
}
