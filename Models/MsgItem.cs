﻿

namespace FF14Chat.Models {
	public class MsgItem {

		public long Id { get; set; }
		public string message { get; set; }
		public long Timestamp { get; set; }
		public int Type { get; set; }
		public long UserId { get; set; }
		public string Username { get; set; }

		public string Time { get; set; }

		public override string ToString() {
			return Username + ": " + message;
		}

	}
}
