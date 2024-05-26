using MessagingApp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MessagingApp.Messages {
	public class MessageManager : MonoBehaviour {
		[SerializeField] private MessageTypes messageType;
		[SerializeField] private TextMeshProUGUI txtMessage;
		[SerializeField] private TextMeshProUGUI txtMessageOwner;
		[SerializeField] private TextMeshProUGUI txtMessageDate;
		[SerializeField] private Image messageBg;

		private MessagesPanelManager messagesPanelManager;
		private float maxWidth;
		private RectTransform txtMessageRect;
		private LayoutElement txtMessageLayoutElement;
		public void Init(MessagesStruct messageInfo, MessagesPanelManager messagesPanelManager) {
			this.messagesPanelManager = messagesPanelManager;

			txtMessage.text = messageInfo.message;
			txtMessageOwner.text = messageInfo.username;
			txtMessageDate.text = messageInfo.date;

			Color invisibleColor = new Color(0, 0, 0, 0);
			ChangeTextColors(invisibleColor);
			ChangeBgColor(invisibleColor);

			maxWidth = messagesPanelManager.MessagesLayoutGroup.gameObject.GetComponent<RectTransform>().rect.width - messagesPanelManager.MessageSettings.MessagesPanelMessagePadding - messagesPanelManager.MessagesLayoutGroup.padding.left - messagesPanelManager.MessagesLayoutGroup.padding.right;
			
			txtMessageRect = txtMessage.GetComponent<RectTransform>();
			txtMessageLayoutElement = txtMessage.GetComponent<LayoutElement>();

			StartCoroutine(CalculateSizeAndShow());
		}

		IEnumerator CalculateSizeAndShow() {
			yield return new WaitForSeconds(0.1f);

			if (txtMessageRect.rect.width > maxWidth) {
				
				txtMessageLayoutElement.preferredWidth = maxWidth;
			}
			else if (txtMessageRect.rect.width < messagesPanelManager.MessageSettings.MessageMinWidth) {
				txtMessageLayoutElement.preferredWidth = messagesPanelManager.MessageSettings.MessageMinWidth;
			}

			if (messageType.Equals(MessageTypes.receivedMessage)) {
				ChangeBgColor(messagesPanelManager.MessageSettings.ReceivedMessageBgColor);
				ChangeTextColors(messagesPanelManager.MessageSettings.ReceivedMessageTextColor);
			}
			else if (messageType.Equals(MessageTypes.sentMessage)) {
				ChangeTextColors(messagesPanelManager.MessageSettings.SentMessageTextColor);
				ChangeBgColor(messagesPanelManager.MessageSettings.SentMessageBgColor);
			}			

			gameObject.transform.parent = messagesPanelManager.MessagesLayoutGroup.gameObject.transform;

			yield return new WaitForSeconds(0.1f);

			messagesPanelManager.ScrollToEnd();
		}
		void ChangeTextColors(Color textColor) {
			txtMessageOwner.color = textColor;
			txtMessage.color = textColor;
			txtMessageDate.color = textColor;
		}
		void ChangeBgColor(Color bgColor) {
			messageBg.color = bgColor;
		}
	}
}

