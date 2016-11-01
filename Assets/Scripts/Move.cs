using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float time = 0.5f;    //seconds per step
	public float step = 0.25f;

	private Animator animator;
	private Rigidbody rigid;
	private float speed = 0; // distance per second
	private float startTime = 0;

	private int direct = 0; //0left 1behind 2rignt 3front
	private float horMove = 0;
	private float verMove = 0;
	private bool isMoving = false;
	private bool isTrigger = false;

	private Vector3 startPos;
	private Vector3 targetPos;

	void Start () {
		rigid = GetComponent<Rigidbody> ();
		targetPos = transform.position;
		startPos = transform.position;
		speed = (float)step / time;
		animator = GetComponent<Animator> ();
		rigid.constraints = RigidbodyConstraints.FreezeRotation;

	}

//	void OnTriggerEnter(Collision otherObject){
//		Debug.Log ("trigger");
//		isTrigger = true;
//	}
	void OnCollisionEnter(Collision other){
		Debug.Log ("collider");
		isMoving = false;
	}
//	void OnTriggerExit(Collider otherObject){
//		isTrigger = false;
//	}
	// Update is called once per frame
	void Update(){
		if (Input.GetKeyUp (KeyCode.A) && !isMoving) {
			Debug.Log ("click");
			rigid.velocity = new Vector3 (-50f, 0, 0);
			isMoving = true;
			StartCoroutine (MoveWait ());
		}
		if (Input.GetKeyUp (KeyCode.D) && !isMoving) {
			Debug.Log ("click");
			rigid.velocity = new Vector3 (50f, 0, 0);
			isMoving = true;
			StartCoroutine (MoveWait ());
		}
		if (Input.GetKeyUp (KeyCode.W) && !isMoving) {
			Debug.Log ("click");
			rigid.velocity = new Vector3 (0, 0, 50f);
			isMoving = true;
			StartCoroutine (MoveWait ());
		}
		if (Input.GetKeyUp (KeyCode.S) && !isMoving) {
			Debug.Log ("click");
			rigid.velocity = new Vector3 (0f, 0, -50f);
			isMoving = true;
			StartCoroutine (MoveWait ());
		}
		if (!isMoving) {
			rigid.velocity = new Vector3 (0, 0, 0);
		}
	}
	IEnumerator MoveWait(){
		float length = 23.0f;
		float covered = 0;
		for(;length-covered>0.1f;covered+=Time.deltaTime*50){
			yield return null;
		}

		//yield return new WaitForSeconds (0.480f);
		isMoving = false;
	}




	void Update_1 () {
		int rotateAngle = 0;
		if (isMoving) {
			float friction = (Time.time - startTime) * speed / step;
//			print (transform.position.ToString () + " " + targetPos.ToString () + " " + friction);

			if (friction >= 0.9f) {
				transform.position = Vector3.Lerp (startPos, targetPos, 1);
				animator.SetBool ("Running", false);
			} else {
				transform.position = Vector3.Lerp (startPos, targetPos, friction);

			}
				
			if (friction > 1) {
				
				horMove = 0;
				verMove = 0;
				isMoving = false;

			}
		}


		if (Input.GetKeyUp (KeyCode.A) && isMoving == false && !isTrigger) {
			horMove = -1.0f;
			rotateAngle = (0 - direct) * 90;
			direct = 0;
			transform.Rotate (Vector3.up * rotateAngle);

			startTime = Time.time;
			isMoving = true;
			startPos = transform.position;
			targetPos = transform.position + new Vector3 (horMove*step,0,verMove*step);
			animator.SetBool ("Running", true);

		}
		if (Input.GetKeyUp (KeyCode.D) && isMoving == false && !isTrigger) {
			horMove = 1.0f;
			rotateAngle = (2 - direct) * 90;
			direct = 2;

			transform.Rotate (Vector3.up * rotateAngle);
			startTime = Time.time;
			isMoving = true;
			startPos = transform.position;
			targetPos = transform.position + new Vector3 (horMove*step,0,verMove*step);
			animator.SetBool ("Running", true);

		}
		if (Input.GetKeyUp (KeyCode.W) && isMoving == false && !isTrigger) {
			verMove = 1.0f;
			rotateAngle = (1 - direct) * 90;
			direct = 1;

			transform.Rotate (Vector3.up * rotateAngle);
			startTime = Time.time;
			isMoving = true;
			startPos = transform.position;
			targetPos = transform.position + new Vector3 (horMove*step,0,verMove*step);
			animator.SetBool ("Running", true);

		}
		if (Input.GetKeyUp (KeyCode.S) && isMoving == false && !isTrigger) {
			verMove = -1.0f;
			rotateAngle = (3 - direct) * 90;
			direct = 3;

			transform.Rotate (Vector3.up * rotateAngle);
			startTime = Time.time;
			isMoving = true;
			startPos = transform.position;
			targetPos = transform.position + new Vector3 (horMove*step,0,verMove*step);
			animator.SetBool ("Running", true);

		}


//		transform.position = transform.position + new Vector3 (horMove*step,0,verMove*step);
	
//		StartCoroutine (MoveSmoothly (horMove, verMove));

	}


}
