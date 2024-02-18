

namespace FF14Chat.Controls {
	public class UserItem {
		public string Id { get; set; }
		public string Name { get; set; }

		public UserItem(string Id, string Name) {
			this.Id = Id;
			this.Name = Name;	
		}
		public override string ToString() {
			return Name;
		}
	}
}
