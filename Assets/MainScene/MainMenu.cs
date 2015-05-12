using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	bool usingPublicMasterServer = true;
	string masterIP = "127.0.0.1";
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		int screenWidth = Screen.width;
		int screenHeight = Screen.height;
		int buttonHeight =  (int)(screenHeight * 0.1f) ;
		int buttonWidth  =  (int)(screenWidth * 0.8f) ;
		int width = 300;
		int height = 200;
		if (width < buttonWidth) 
		{
			width = buttonWidth;
		}
		if (height < buttonHeight * 5) 
		{
			height = buttonHeight * 5 ;
		}

		GUILayout.BeginArea(new Rect(Screen.width/2 -  width/2 ,Screen.height/2 - height ,width ,height));

		GUILayout.FlexibleSpace();	

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();	
		GUILayout.Label("Unity Networking Demo");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.Space(10);

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Tutorial 1 - P2P", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))){
			Application.LoadLevel(1);
		}	
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Tutorial 1 - Join", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth))){
			Application.LoadLevel(2);
		}	
		GUILayout.EndHorizontal();

		if (usingPublicMasterServer) 
		{
			GUILayout.BeginHorizontal();
			usingPublicMasterServer = GUILayout.Toggle(usingPublicMasterServer, "Unity Master Server", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			MasterServer.ipAddress = "0.0.0.0";
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("IP:"+MasterServer.ipAddress, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			GUILayout.EndHorizontal();
		} else {
			GUILayout.BeginHorizontal();
			usingPublicMasterServer = GUILayout.Toggle(usingPublicMasterServer, "Private Master Server", GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			masterIP = GUILayout.TextField (masterIP, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth));
			MasterServer.ipAddress = masterIP;
			GUILayout.EndHorizontal();
		}


		GUILayout.EndArea();
	}
}
