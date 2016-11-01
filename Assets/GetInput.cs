using UnityEngine;
using System.Collections;

//合作代码提交
//合作代码提交二
public class GetInput : MonoBehaviour {
	Robert robert;
	// Use this for initialization
	void Start () {
		robert = GetComponent<Robert> ();
		if (robert == null) {
			Debug.LogError ("No Robert.cs script");
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(0,0,0);
		int direction = robert.direction;
		if (Input.GetKeyUp (KeyCode.A)) {
			movement = new Vector3 (0, 0, 1.0f);
			direction = 0;
		} else if (Input.GetKeyUp (KeyCode.D)) {
			movement = new Vector3 (0, 0, -1.0f);
			direction = 2;
		} else if (Input.GetKeyUp (KeyCode.W)) {
			movement = new Vector3 (1.0f, 0, 0);
			direction = 1;
		} else if (Input.GetKeyUp (KeyCode.S)) {
			movement = new Vector3 (-1.0f, 0, 0);
			direction = 3;
		}
		robert.Move (ref movement, direction);
		
	}
}
