using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessagingApp.Messages {
	[CreateAssetMenu(fileName = "MessageSettingsSo", menuName = "Settings/MessageSettingsSo", order = 1)]
	public class MessageSettingsSo : ScriptableObject {
		[SerializeField] private GameObject receivedMessagePrefab;
		[SerializeField] private GameObject sentMessagePrefab;

		[SerializeField] private Color receivedMessageBgColor = new Color();
		[SerializeField] private Color receivedMessageTextColor = new Color();

		[SerializeField] private Color sentMessageBgColor = new Color();
		[SerializeField] private Color sentMessageTextColor = new Color();

		[SerializeField] private float messagesPanelMessagePadding = 50f;
		[SerializeField] private float messageMinWidth = 100f;

		public GameObject ReceivedMessagePrefab => receivedMessagePrefab;
		public GameObject SentMessagePrefab => sentMessagePrefab;
		public Color ReceivedMessageBgColor => receivedMessageBgColor;
		public Color ReceivedMessageTextColor => receivedMessageTextColor;
		public Color SentMessageBgColor => sentMessageBgColor;
		public Color SentMessageTextColor => sentMessageTextColor;
		public float MessagesPanelMessagePadding => messagesPanelMessagePadding;
		public float MessageMinWidth => messageMinWidth;
	}
}

