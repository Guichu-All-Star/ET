using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.CreatePlayerFinish)]
	[Event(EventIdType.LoginFinish)]
	public class CreatePlayerFinish_CreateLobbyUI: AEvent
	{
		public override void Run()
		{
			UI ui = UILobbyFactory.Create();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
		}
	}
}
