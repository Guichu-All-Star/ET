using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.RegisterStart)]
	public class RegisterStart_CreateRegisterUI: AEvent
	{
		public override void Run()
		{
			UI ui = UIRegisterFactory.Create();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
			Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILogin);
		}
	}
}
