using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2R_Register)]
	public partial class C2R_Register : IRequest {}

	[Message(HotfixOpcode.R2C_Register)]
	public partial class R2C_Register : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

	[Message(HotfixOpcode.C2G_CreatePlayer)]
	public partial class C2G_CreatePlayer : IRequest {}

	[Message(HotfixOpcode.G2C_CreatePlayer)]
	public partial class G2C_CreatePlayer : IResponse {}

//gate向realm发送用户上线消息
	[Message(HotfixOpcode.G2R_PlayerOnline)]
	public partial class G2R_PlayerOnline : IMessage {}

//gate向realm发送用户下线消息
	[Message(HotfixOpcode.G2R_PlayerOffline)]
	public partial class G2R_PlayerOffline : IMessage {}

//realm向gate发送踢出用户消息
	[Message(HotfixOpcode.R2G_KickOutPlayer)]
	public partial class R2G_KickOutPlayer : IRequest {}

//gate向realm回复踢出用户消息
	[Message(HotfixOpcode.G2R_KickOutPlayer)]
	public partial class G2R_KickOutPlayer : IResponse {}

	[Message(HotfixOpcode.C2G_ClientLogout)]
	public partial class C2G_ClientLogout : IMessage {}

//Gate向客户端发送断开连接消息
	[Message(HotfixOpcode.G2C_KickOut)]
	public partial class G2C_KickOut : IMessage {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2R_Register = 10003;
		 public const ushort R2C_Register = 10004;
		 public const ushort C2G_LoginGate = 10005;
		 public const ushort G2C_LoginGate = 10006;
		 public const ushort G2C_TestHotfixMessage = 10007;
		 public const ushort C2M_TestActorRequest = 10008;
		 public const ushort M2C_TestActorResponse = 10009;
		 public const ushort PlayerInfo = 10010;
		 public const ushort C2G_PlayerInfo = 10011;
		 public const ushort G2C_PlayerInfo = 10012;
		 public const ushort C2G_CreatePlayer = 10013;
		 public const ushort G2C_CreatePlayer = 10014;
		 public const ushort G2R_PlayerOnline = 10015;
		 public const ushort G2R_PlayerOffline = 10016;
		 public const ushort R2G_KickOutPlayer = 10017;
		 public const ushort G2R_KickOutPlayer = 10018;
		 public const ushort C2G_ClientLogout = 10019;
		 public const ushort G2C_KickOut = 10020;
	}
}
