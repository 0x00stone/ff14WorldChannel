
using System.Threading;
using GreyMagic;
using System;
using System.Text;
using FF14Chat.Common;

namespace FF14Chat.Actions {
	public class Command {
		private IntPtr ProcessChatBoxPtr;
		private int ModuleOffset;
		private IntPtr UiModule;

		protected ExternalProcessMemory Memory;
		protected SigScanner SigScanner;

		public Command() {}

		public void Setup(SigScanner SigScanner, ExternalProcessMemory Memory) {
			this.SigScanner = SigScanner;
			this.Memory = Memory;
			Log.info("SigScanner" + SigScanner.ToString());
			Log.info("Memory" + Memory.ToString());


			try {
				//Compatible with some plugins of Dalamud
				//ProcessChatBoxPtr = _scanner.ScanText("40 53 56 57 48 83 EC 70 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B 02");
				ProcessChatBoxPtr = SigScanner.ScanText("E8 ?? ?? ?? ?? FE 86 ?? ?? ?? ?? C7 86");

				ModuleOffset = SigScanner.ReadInt32(SigScanner.ScanText("48 8D 8F ?? ?? ?? ?? 4C 8B C7 48 8D 54 24 ??") + 3);
				UiModule = SigScanner.ReadIntPtr(SigScanner.ReadIntPtr(SigScanner.GetStaticAddressFromSig("48 8B 05 ?? ?? ?? ?? 48 8B D9 8B 40 14 85 C0")));
				FF14Chat_Main.isReady = Memory != null;
				Log.error("command getoffset success");
			} catch(Exception ex) {
				Log.error("command getoffset error:" + ex.ToString());
				FF14Chat_Main.isReady = false;
			}
			//Log("初始化完成");
		}


		public void DoTextCommand(string command) {
			Log.info("command: " + command);
			if(!FF14Chat_Main.isReady) {
				Log.error("没有对应的游戏进程");
				return;
			}

			if(command == "") {
				Log.error("command 指令为空");
				return;
			}

			// 去掉command中的所有换行符
			command = command.Replace("\n", "").Replace("\r", "");

			const int maxByteLength = 180;
			string ignoredPortion = null;

			// 检查command的Unicode字节长度是否超过180字节
			if(Encoding.UTF8.GetByteCount(command) > maxByteLength) {
				byte[] bytes = Encoding.UTF8.GetBytes(command);
				int maxBytes = maxByteLength;

				// 确保不会截断一个Unicode字符
				while(maxBytes > 0 && (bytes[maxBytes] & 0xC0) == 0x80) {
					maxBytes--;
				}

				string truncatedCommand = Encoding.UTF8.GetString(bytes, 0, maxBytes);
				ignoredPortion = command.Substring(truncatedCommand.Length);
				command = truncatedCommand;
			}

			if(!string.IsNullOrEmpty(ignoredPortion)) {
				Log.error("DoTextCommand: "+ $"上一条命令中，文本\"{ignoredPortion}\"被忽略，因为系统宏的限制在180个字节以内。");
			}

			var assemblyLock = Memory.Executor.AssemblyLock;

			var flag = false;
			try {
				Monitor.Enter(assemblyLock, ref flag);
				var array = Encoding.UTF8.GetBytes(command);
				AllocatedMemory allocatedMemory = Memory.CreateAllocatedMemory(400), allocatedMemory2 = Memory.CreateAllocatedMemory(array.Length + 30);
				allocatedMemory2.AllocateOfChunk("cmd", array.Length);
				allocatedMemory2.WriteBytes("cmd", array);
				allocatedMemory.AllocateOfChunk<IntPtr>("cmdAddress");
				allocatedMemory.AllocateOfChunk<long>("t1");
				allocatedMemory.AllocateOfChunk<long>("tLength");
				allocatedMemory.AllocateOfChunk<long>("t3");
				allocatedMemory.Write("cmdAddress", allocatedMemory2.Address);
				allocatedMemory.Write("t1", 0x40);
				allocatedMemory.Write("tLength", array.Length + 1);
				allocatedMemory.Write("t3", 0x00);
				_ = Memory.CallInjected64<int>(ProcessChatBoxPtr, UiModule + ModuleOffset, allocatedMemory.Address, UiModule);

				Log.info("command send message success: " + command);
				Log.info("Memory: " + Memory.ToString());
				Log.info("ProcessChatBoxPtr: " + ProcessChatBoxPtr.ToString());
				Log.info("UiModule + ModuleOffset: " + (UiModule + ModuleOffset).ToString());
				Log.info("allocatedMemory.Address: " + allocatedMemory.Address.ToString());
				Log.info("UiModule: " + UiModule.ToString());
			} catch (Exception e){
				Log.info("command send message fail: " + command+" error: "+e.Message);
			} finally {
				if(flag) Monitor.Exit(assemblyLock);
			}
		}
	}
}
