using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF14Chat.Controls {
	internal class SerializeUtil {
		public static string ToJson(object model) {
			var serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented });
			var writer = new StringWriter();
			serializer.Serialize(writer, model);
			return writer.ToString();
		}
	}
}
