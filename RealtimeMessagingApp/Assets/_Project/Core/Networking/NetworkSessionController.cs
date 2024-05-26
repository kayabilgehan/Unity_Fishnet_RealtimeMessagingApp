using FishNet.Connection;
using FishNet.Example.Scened;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using MessagingApp.Core;
using TMPro;
using UnityEngine;
using VContainer;

namespace MessagingApp.Core {
	public class NetworkSessionController : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI txtPlayerInfo;

		private static NetworkSessionController _instance;
		private GameSettingsSo _gameSettings;
		private NetworkManager _networkManager;
		private PlayerController _localPlayerController;
		private LocalConnectionState _clientState = LocalConnectionState.Stopped;
		private LocalConnectionState _serverState = LocalConnectionState.Stopped;

		public static NetworkSessionController Instance => _instance;
		public PlayerController LocalPlayerController => _localPlayerController;

		public void SetLocalPlayer(PlayerController localPlayer) {
			_localPlayerController = localPlayer;
			SetPlayerName();
		}
		private void OnDestroy() {
			if (_networkManager == null)
				return;

			_networkManager.ServerManager.OnServerConnectionState -= ServerManager_OnServerConnectionState;
			_networkManager.ClientManager.OnClientConnectionState -= ClientManager_OnClientConnectionState;
		}
		private void Awake() {
			_instance = this;
		}
		void Start() {
			_networkManager = FindObjectOfType<NetworkManager>();
			if (_networkManager == null) {
				Debug.LogError("NetworkManager not found.");
				return;
			}
			else {
				_networkManager.ServerManager.OnServerConnectionState += ServerManager_OnServerConnectionState;
				_networkManager.ClientManager.OnClientConnectionState += ClientManager_OnClientConnectionState;
			}

			if (_gameSettings.IsServer) {
				if (_networkManager == null)
					return;

				if (_serverState != LocalConnectionState.Stopped)
					_networkManager.ServerManager.StopConnection(true);
				else
					_networkManager.ServerManager.StartConnection();
			}
			else {
				if (_networkManager == null)
					return;

				if (_clientState != LocalConnectionState.Stopped)
					_networkManager.ClientManager.StopConnection();
				else
					_networkManager.ClientManager.StartConnection();
			}
		}
		private void ClientManager_OnClientConnectionState(ClientConnectionStateArgs obj) {
			_clientState = obj.ConnectionState;
		}
		private void SetPlayerName() {
			txtPlayerInfo.text = "Player: " + _localPlayerController.ClientId;
		}

		private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs obj) {
			_serverState = obj.ConnectionState;
		}

		void Update() {

		}

		[Inject]
		void Construct(GameSettingsSo gameSettings) {
			this._gameSettings = gameSettings;
		}
	}
}

