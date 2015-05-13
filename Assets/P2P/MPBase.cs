using UnityEngine;
using System.Collections;
using System;

public class MPBase : MonoBehaviour
{

	public string connectToIp = "127.0.0.1";
    public int connectPort = 25001;
    public bool useNAT = false;
    public string ipaddress = "";
    public string port = "";

    string playerName = "testName";

	// Use this for initialization
	void Start () {
		float seed = System.DateTime.Now.Millisecond;
		UnityEngine.Random.seed = (int)Math.Ceiling(seed);
		float value = UnityEngine.Random.value * 100;
		print ("Start program" + seed +":" + value);
		playerName = "user" + Math.Ceiling(value).ToString ();
		print ("Msater ip:" + MasterServer.ipAddress + "port:" + MasterServer.port);
	}
    // 
    void OnGUI() 
    {
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		int buttonHeight =  (int)(screenHeight * 0.1f) ;
		int buttonWidth  =  (int)(screenWidth * 0.2f) ;
		GUI.skin.textField.fontSize = (int)(buttonHeight * 0.6f); //控制Lable字體大小
		GUI.skin.button.fontSize = (int)(buttonHeight * 0.6f); //控制Button字體大小
		int width = 300;
//		int height = 200;
		if (buttonWidth < 100 ) 
		{
			buttonWidth = 100;
		}
		if (width < buttonWidth) 
		{
			width = buttonWidth;
		}


		if(GUILayout.Button("MainMenu", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))){
			Network.Disconnect(200);
			Application.LoadLevel(0);
		}	
		if (Network.peerType == NetworkPeerType.Disconnected)
        {
			if (GUILayout.Button("Connect", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
            {
				print ("Connect Server");
                if (playerName != "testName")
                {
                    //Network.useNat = useNAT;
                    Network.Connect(connectToIp, connectPort);
                    PlayerPrefs.SetString("playerName", playerName);
                }
            }
			if (GUILayout.Button("Start Server", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
            {
                if (playerName != "testName")
                {
                    //Network.useNat = useNAT;
                    Network.InitializeServer(32, connectPort, useNAT);
					MasterServer.dedicatedServer = true;
					MasterServer.RegisterHost("xpecP2PGames", playerName, "l33t game for all");

                   foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
                   {
                       go.SendMessage("OnNettworkLoadedLevel",SendMessageOptions.DontRequireReceiver);
                   }
                   PlayerPrefs.SetString("playerName", playerName);
                }
            }
			playerName = GUILayout.TextField(playerName, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			connectToIp = GUILayout.TextField(connectToIp, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			connectPort = Convert.ToInt32(GUILayout.TextField(connectPort.ToString(), GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)));
        }
        else
        {
            if (Network.peerType == NetworkPeerType.Connecting) GUILayout.Label("Connect Status: Connecting");
            else if(Network.peerType == NetworkPeerType.Client)
            {
                GUILayout.Label("Connection Status:Client");
                GUILayout.Label("Ping to Server:" + Network.GetAveragePing(Network.connections[0]));
            }
            else if(Network.peerType == NetworkPeerType.Server)
            {
                GUILayout.Label("Connection Status:Server");
                GUILayout.Label("Connections:"+Network.connections.Length);
                if (Network.connections.Length >= 1)
                    GUILayout.Label("Ping to Server:" + Network.GetAveragePing(Network.connections[0]));
            }

			if(GUILayout.Button("Disconnect", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
                Network.Disconnect(200);

            ipaddress = Network.player.ipAddress;
            port = Network.player.port.ToString();
            GUILayout.Label("IP Address:" + ipaddress + ":" + port);
        }
	
	}

	public int currentHealth;

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
		int health = 0;
		print ("OnSerializeNetworkView");
		if (stream.isWriting) {
			health = currentHealth;
			stream.Serialize(ref health);
		} else {
			stream.Serialize(ref health);
			currentHealth = health;
		}
	}

}
