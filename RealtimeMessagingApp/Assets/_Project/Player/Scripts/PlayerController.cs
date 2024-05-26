using FishNet.Example.Scened;
using FishNet.Object;
using MessagingApp.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
	private NetworkSessionController networkworkSessionController;

	public int ClientId => Owner.ClientId;

	private void Awake() {
		networkworkSessionController = FindObjectOfType<NetworkSessionController>();
	}
	public override void OnStartClient() {
		base.OnStartClient();
		if (!base.IsOwner) {
			this.enabled = false;
		}
		else {
			networkworkSessionController.SetLocalPlayer(this);
		}
	}
}
