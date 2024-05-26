using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using FishNet.Transporting;
using MessagingApp.Core;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MessagingApp.Messages {
	public class MessagesPanelManager : MonoBehaviour {
		[SerializeField] private RectTransform messagesPanel;
		[SerializeField] private TMP_InputField txtMessageInput;
		[SerializeField] private ScrollRect messagesListScrollRect;

		private MessageSettingsSo messageSettings;
		private GameSettingsSo gameSettings;
		private LayoutGroup messagesLayoutGroup;

		public MessageSettingsSo MessageSettings => messageSettings;
		public LayoutGroup MessagesLayoutGroup => messagesLayoutGroup;

		public void ScrollToEnd() {
			messagesListScrollRect.normalizedPosition = new Vector2(0, 0);
		}
		public void MessageSendClicked() {
			SendMessage();
		}

		private void OnEnable() {
			InstanceFinder.ClientManager.RegisterBroadcast<MessagesStruct>(OnBroadcastPackageReceived);
			InstanceFinder.ServerManager.RegisterBroadcast<MessagesStruct>(OnClientPackageReceived);
		}
		private void OnDisable() {
			InstanceFinder.ClientManager.UnregisterBroadcast<MessagesStruct>(OnBroadcastPackageReceived);
			InstanceFinder.ServerManager.UnregisterBroadcast<MessagesStruct>(OnClientPackageReceived);
		}
		private void OnBroadcastPackageReceived(MessagesStruct msg, Channel channel) {
			ShowMessage(msg);
		}
		private void OnClientPackageReceived(NetworkConnection networkConnection, MessagesStruct msg, Channel channel) {
			InstanceFinder.ServerManager.Broadcast(msg);
		}
		private void SendMessage() {
			MessagesStruct msg = new MessagesStruct() {
				clientId = NetworkSessionController.Instance.LocalPlayerController.ClientId,
				message = txtMessageInput.text,
				username = NetworkSessionController.Instance.LocalPlayerController.ClientId.ToString(),
				date = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
			};
			txtMessageInput.text = "";
			if (InstanceFinder.IsServer) {
				InstanceFinder.ServerManager.Broadcast(msg);
			}
			else if (InstanceFinder.IsClient) {
				InstanceFinder.ClientManager.Broadcast(msg);
			}
			else {
				Debug.Log("Not connected as a client or server.");
			}
		}
		private void ShowMessage(MessagesStruct message) {
			MessageManager newMessageManager;
			if (message.clientId == NetworkSessionController.Instance.LocalPlayerController.ClientId) {
				GameObject newMessage = Instantiate(messageSettings.SentMessagePrefab);
				newMessageManager = newMessage.GetComponent<MessageManager>();
				message.username = "You";
			}
			else {
				GameObject newMessage = Instantiate(messageSettings.ReceivedMessagePrefab);
				newMessageManager = newMessage.GetComponent<MessageManager>();
			}
			newMessageManager.Init(message, this);

			LayoutRebuilder.ForceRebuildLayoutImmediate(messagesPanel);
		}
		
		private void Update() {
			if (Input.GetKeyDown(KeyCode.Return)) {
				SendMessage();
			}
		}
		private void Start() {
			messagesLayoutGroup = messagesPanel.GetComponent<LayoutGroup>();
		}
		[Inject]
		private void Construct(MessageSettingsSo messageSettings, GameSettingsSo gameSettings) {
			this.messageSettings = messageSettings;
			this.gameSettings = gameSettings;
		}
	}
}