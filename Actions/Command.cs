
using System.Text;
using FF14Chat.Common;

namespace FF14Chat.Actions {
	public class Command {

		const int maxByteLength = 180;
		public void DoTextCommand(string command) {

			if(command == "") {
				Log.error("command 指令为空");
				return;
			}

			Log.info("command: " + command);
			command = command.Replace("\n", "").Replace("\r", "");


			// 检查command的Unicode字节长度是否超过180字节
			if(Encoding.UTF8.GetByteCount(command) > maxByteLength) {
				Log.error($"DoTextCommand: 上一条命令中，文本{command}被忽略，因为系统宏的限制在180个字节以内。");
			} else {
				Network.NetworkUtil.SendPostRequest("http://127.0.0.1:1002/command",command);
				Log.debug($"sendToPostNamazu: {command}");
				/*if(PostNamazu!=null) {
					Log.debug($"sendToPostNamazu: {command}");
					PostNamazu.DoAction("command", command);
				}*/
			}
		}
	}
}
