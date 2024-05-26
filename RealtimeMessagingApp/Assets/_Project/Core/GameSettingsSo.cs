using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessagingApp.Core {
	[CreateAssetMenu(fileName = "GameSettingsSo", menuName = "Settings/GameSettingsSo", order = 1)]
	public class GameSettingsSo : ScriptableObject {
		[SerializeField] private bool isServer = false;

		public bool IsServer => isServer;
	}
}
