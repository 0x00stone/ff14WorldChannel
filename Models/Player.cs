using System.Runtime.InteropServices;

namespace FF14Chat.Models{

	// Client::Game::UI::PlayerState
	// ctor "48 81 C1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? C6 83"
	[StructLayout(LayoutKind.Explicit, Size = 0x140)]
	public unsafe partial struct Player {
		[FieldOffset(0x01)] public fixed byte CharacterName[64];
		[FieldOffset(0x41)] public fixed byte PSNOnlineID[17];
		[FieldOffset(0x54)] public uint ObjectId;
		[FieldOffset(0x58)] public ulong ContentId;

		[FieldOffset(0x6B)] public byte Sex;
		[FieldOffset(0x6C)] public byte Race;
		[FieldOffset(0x6D)] public byte Tribe;

		[FieldOffset(0x136)] public byte GuardianDeity;
		[FieldOffset(0x137)] public byte BirthMonth;
		[FieldOffset(0x138)] public byte BirthDay;
		[FieldOffset(0x13A)] public byte StartTown;
	}
}