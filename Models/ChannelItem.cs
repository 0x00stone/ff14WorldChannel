

namespace FF14Chat.Controls {
	internal class ChannelItem {
		public string channelName { get; set; }
		public string channelString { get; set; }

		public ChannelItem(string channelName, string channelString) {
			this.channelName = channelName;
			this.channelString = channelString;
		}
		public override string ToString() {
			return channelName;
		}
	}
}
