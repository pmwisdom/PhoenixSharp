﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace Phoenix {

	/** Instead of leaking JSON dependency everywhere, we simply group all JSON related
	 * serialization in extensions, which can easily be altered later.
	 */
	public static class MessageSerialization {

		public static string Serialize(this Message message) {

			var json = JObject.FromObject(message);
			// we set an empty object instead of null for payload
			// not sure if this is required
			if (json["payload"].Type == JTokenType.Null) {
				json["payload"] = new JObject();
			}
			
			return json
				.ToString(Newtonsoft.Json.Formatting.None);
		}

		public static Message Deserialize(string data) {
			return JObject
				.Parse(data)
				.ToObject<Message>();
		}
	}
}
