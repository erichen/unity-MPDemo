using UnityEngine;
using System.Collections;

public class TestMasterServer : MonoBehaviour {
	bool connectToServer = false;
	void Awake() {
		print ("Msater ip:" + MasterServer.ipAddress + "port:" + MasterServer.port);
//		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.RequestHostList("xpecP2PGames");
	}
	void OnConnectedToServer() 
	{
		connectToServer = true;
		Debug.Log("Connected to server");
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		connectToServer = false;
	}
	
	void OnGUI() {
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		int buttonHeight =  (int)(screenHeight * 0.1f) ;
		int buttonWidth  =  (int)(screenWidth * 0.2f) ;
		int width = 300;
//		int height = 200; 
		if (width < buttonWidth) 
		{
			width = buttonWidth;
		}

		GUILayout.BeginHorizontal();    
		if(GUILayout.Button("MainMenu", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))){
			Network.Disconnect(200);
			Application.LoadLevel(0);
			connectToServer = false;
			return ;
		}	
		if (connectToServer) 
		{
			if(GUILayout.Button("Disconnect", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
				Network.Disconnect(200);
			return;
		}
		if (GUILayout.Button ("Refresh", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))) {
			MasterServer.RequestHostList("xpecP2PGames");
		}
		GUILayout.EndHorizontal(); 

		DispalyHostList ();

	}
	void DispalyHostList()
	{
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		int buttonHeight =  (int)(screenHeight * 0.1f) ;
		int buttonWidth  =  (int)(screenWidth * 0.2f) ;
		int width = 300;
//		int height = 200;
		if (width < buttonWidth) 
		{
			width = buttonWidth;
		}

		HostData[] data = MasterServer.PollHostList ();
		// Go through all the hosts in the host list
		foreach (var element in data) {
				GUILayout.BeginHorizontal ();    
				var name = element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
				GUILayout.Label (name, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));  
				GUILayout.Space (5);
				string hostInfo;
				hostInfo = "[";
				foreach (var host in element.ip)
						hostInfo = hostInfo + host + ":" + element.port + " ";
				hostInfo = hostInfo + "]";
				GUILayout.Label (hostInfo, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));  
				GUILayout.Space (5);
				GUILayout.Label (element.comment, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
				GUILayout.Space (5);
				GUILayout.FlexibleSpace ();
				if (GUILayout.Button ("Connect", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))) {
						// Connect to HostData struct, internally the correct method is used (GUID when using NAT).
						Network.Connect (element);           
				}
				GUILayout.EndHorizontal ();  
		}
	}
}
