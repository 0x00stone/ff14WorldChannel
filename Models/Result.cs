

namespace FF14Chat.Models {
	public class LoginUserResult {
		private string token;
		private string serverId;
		private string aliasname;
		private string password;
		private string content;

		public LoginUserResult(string serverId, string token, string aliasname, string password) {
			this.token = token;
			this.serverId = serverId;
			this.aliasname = aliasname;
			this.password = password;
		}

		public void setContent(string content) {
			this.content = content;
		}

		public string getcontent() {
			return this.content;
		}


		public string getServerId() {
			return this.serverId;
		}

		public string getToken() {
			return this.token;
		}
		public string getAliasname() {
			return this.aliasname;
		}

		public string getPassword() {
			return this.password;
		}

		public void setServerId(string serverId) {
			this.serverId = serverId;
		}

		public void setToken(string token) { 
			this.token=token;
		}

		public void setAliasName(string aliasname) {
			this.aliasname = aliasname;
		}

		public void setPassword(string password) {
			this.password = password;
		}


	}
}
