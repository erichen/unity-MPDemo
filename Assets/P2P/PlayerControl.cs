using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float speed = 5;
	public GameObject particle;

	// Use this for initialization
	void Start () {
		print ("Start isMine" + networkView.isMine);
		if (! networkView.isMine) {
			enabled = false;
		}
		Input.simulateMouseWithTouches = true;
	}
	// 

	// Update is called once per frame
	void Update () 	{
		if (networkView.isMine) {
			checkMouse();
			checkTouch ();
			Vector3 moveDir = new Vector3(Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			if (moveDir == Vector3.zero){
				return;
			}
			transform.Translate(speed * moveDir * Time.deltaTime );
		}
	}
	void checkMouse(){
		if (Input.GetMouseButtonDown (0)) {//left button
			Vector3 pos = Input.mousePosition;
			MoveTo(pos.x, pos.y);
		}
	}
	void checkTouch(){
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				Vector2 pos = touch.position;
				MoveTo(pos.x, pos.y);
			}
		}
	}
	void MoveTo(float x, float y){
		Vector3 moveDir = GetMoveDir(x, y);
		transform.Translate(speed * moveDir * Time.deltaTime );
	}
	Vector3 GetMoveDir(float px, float py){
		float x = -1;
		float y = -1;
		float jump = 0;
		if (px > Screen.width * 0.25){
			x = 0; 
		}
		if (px > Screen.width * 0.75){
			x = 1; 
		}
		if (py > Screen.height * 0.25){
			y = 0; 
		}
		if (py > Screen.height * 0.75){
			y = 1; 
		}
		if (x == 0 && y == 0){
			jump = 1;
		}
		
		Vector3 moveDir = new Vector3(x, jump * 10, y);
		return moveDir;
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)	{
		if (stream.isWriting) {
				Vector3 pos = transform.position;
				stream.Serialize (ref pos);
		} else {
				Vector3 posRec = Vector3.zero;
				stream.Serialize (ref posRec);
				transform.position = posRec;
		}
	}
}