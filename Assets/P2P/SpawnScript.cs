using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public Transform player;

	void OnServerInitialized()
	{
		SpawnPlayer ();
	}

	void OnConnectedToServer()
	{
		SpawnPlayer ();
	}

	void SpawnPlayer()
	{
		print ("SpawnPlayer" + Network.player);
		Network.Instantiate (player, transform.position, transform.rotation, 0);
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		print ("OnPlayerDisconnected" + Network.player);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
