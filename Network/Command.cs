
using System.Text;
using FF14Chat.Common;

namespace FF14Chat.Network {
	public class Command {

		const int maxByteLength = 180;

		public static void DoTextCommand(string command) {
			Log.info("command: " + command);
			if(!FF14Chat_Main.isGameOn) {
				Log.error("没有对应的游戏进程");
				return;
			}

			if(command == "") {
				Log.error("command 指令为空");
				return;
			}

			// 去掉command中的所有换行符
			command = command.Replace("\n", "").Replace("\r", "");


			// 检查command的Unicode字节长度是否超过180字节
			if(Encoding.UTF8.GetByteCount(command) > maxByteLength) {
				Log.error($"DoTextCommand: 上一条命令中，文本{command}被忽略，因为系统宏的限制在180个字节以内。");
			} else {
				Log.debug($"sendToPostNamazu: {command}");
				NetworkUtil.sendToPostNamazu(command);
			}
		}
	}
}
