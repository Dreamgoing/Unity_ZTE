using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public GameObject robert;
	public GameObject parentObj;

	Animator animator;
	Vector3 parentCenter;
	Collider parentCol;
	Robert robertScript;
	bool state;

	// Use this for initialization
	void Start () {
		if(parentObj == null)
			parentObj = transform.parent.gameObject;
		if (robert == null) {
			Debug.LogError ("no robert");
		}
		if (parentObj == null) {
			Debug.LogError ("no parent gameobsject");
		}
		parentCenter = parentObj.transform.position + new Vector3 (0, 5, 0);
		robertScript = robert.GetComponent<Robert> ();
		animator = GetComponent<Animator> ();
		if (robertScript == null) {
			Debug.LogError ("no robert script found");
		}
		if (animator == null) {
			Debug.LogError ("no animator  found");
		}

		parentCol = parentObj.GetComponent<Collider> ();
		state = false;
//		Debug.Log (parentCenter.x);
//		Debug.Log (parentCenter.y);
//		Debug.Log (parentCenter.z);

	}

	// Update is called once per frame
	void Update () {
		if (!IsNear (robert.transform.position)) {
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (parentCol.Raycast (ray, out hit, 100.0f)) {
//				Debug.Log ("hit");
				//if (hit.collider.name == parentObj.name) {
					ChangeSwitch ();

				//}

			}
		}
		
	}
	bool IsNear(Vector3 pos){
		return (Vector3.Magnitude(pos - parentCenter) <= robertScript.stepLength/2);
	}
	void ChangeSwitch(){
		state = !state;
		animator.SetBool ("SwitchOn",state);
	}
}
