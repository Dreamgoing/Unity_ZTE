using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	private Vector3 targetPos;
	private Vector3 deltaPos;
	void Start () {
		if (target != null) {
			deltaPos = transform.position - target.transform.position;
		}
	}
	
	void Update () {
		if (target != null) {
			targetPos = target.transform.position;
		}
		transform.position = targetPos + deltaPos;
	}
}
