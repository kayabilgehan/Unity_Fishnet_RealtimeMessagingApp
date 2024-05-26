using FishNet.Broadcast;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessagingApp.Messages {
	[Serializable]
	public struct MessagesStruct : IBroadcast {
		public int clientId;
		public string username;
		public string message;
		public string date;
	}
}