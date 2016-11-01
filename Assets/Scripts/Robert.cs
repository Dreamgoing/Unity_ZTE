using UnityEngine;
using System.Collections;

public class Robert : MonoBehaviour {

	public float stepLength = 1.0f;
	public int direction = 0;//0z+, 1x+,2z-,3x-
	public float moveSpeed = 2.0f;
	public float bufferTime = 0.3f;
	enum animations{
		idle,running,climbing
	};

	Animator animator;
	bool isMoving = false;
	Vector3 targetPos;
	Vector3 initPos;
	Rigidbody rigid;

//	void OnCollisionStay(Collision col){
//		UpdateAnimation (animations.idle);
//		rigid.velocity = new Vector3 (0, 0, 0);
//		transform.position = initPos;
//		isMoving = false;
//	}
	void OnTriggerEnter(Collider col){
		UpdateAnimation (animations.idle);
		rigid.velocity = new Vector3 (0, 0, 0);
		transform.position = initPos;
		isMoving = false;
	}

	// Use this for initialization
	void Start () {
		targetPos = transform.position;
		initPos = transform.position;
		rigid = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		if (rigid == null) {
			Debug.LogError ("No rigidbody component");
		}
		if (animator == null) {
			Debug.LogError ("No animator component");
		}

		rigid.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Move(ref Vector3 movement,int dir){
		if (isMoving) {
			return;
		}
		if (movement == new Vector3(0,0,0)) {
			return;
		}
		isMoving = true;
		if (movement.magnitude > 1) {
			movement.Normalize ();
		}
		targetPos = transform.position + movement*stepLength;
		initPos = transform.position;

		UpdateAnimation (animations.running);
		StartCoroutine (MoveDelay (dir));

	}
		
	IEnumerator MoveDelay(int dir){
		float rotateAng = (dir - direction) * 90;
		Vector3 maxV = (targetPos - transform.position);
		maxV.Normalize ();
		maxV *= moveSpeed;

		Vector3 startPos = transform.position;
		float friction = 0;

		direction = dir;
		transform.Rotate (Vector3.up * rotateAng);
		yield return new WaitForSeconds (bufferTime);

		//start smoothly
//		while (Vector3.Magnitude(transform.position - startPos) <= stepLength * 0.3f) {
//			rigid.velocity = Vector3.Lerp(maxV * 0.1f,maxV,friction);
//			friction = Vector3.Magnitude (transform.position - startPos) / (stepLength * 0.3f);
//			yield return null;
//		}


		rigid.velocity = maxV;
//		friction = 0;
//		while (Vector3.Magnitude(transform.position - startPos) < stepLength) {
//			if (friction > 0.8f) {
//				friction = 1.0f;
//			}
//			rigid.velocity = Vector3.Lerp(maxV,new Vector3(0,0,0),friction);
//			friction = Vector3.Magnitude (transform.position - startPos)  / stepLength;
//			yield return null;
//
//		}

		//&& Vector3.Magnitude (transform.position - startPos) > stepLength * 0.50f
		while (Vector3.Magnitude (transform.position - startPos) < stepLength * 0.99f ) {
		//	rigid.velocity = Vector3.Lerp(maxV,new Vector3(0,0,0),friction);
			if (Vector3.Magnitude (transform.position - startPos) >= stepLength * 0.90f) {
				UpdateAnimation (animations.idle);
				friction = 0.8f;
			} else if (Vector3.Magnitude (transform.position - startPos) >= stepLength * 0.80f) {
				friction = 0.6f;
			} else if (Vector3.Magnitude (transform.position - startPos) >= stepLength * 0.70f) {
				friction = 0.4f;
			} else if (Vector3.Magnitude (transform.position - startPos) >= stepLength * 0.60f) {
				friction = 0.2f;
			} else {
				friction = 0.1f;
			}
			yield return null;

			
		}
		rigid.velocity = new Vector3 (0, 0, 0);
		transform.position = targetPos;
		isMoving = false;
	}


	void UpdateAnimation(animations anims){
		switch (anims) {
			case animations.idle:
				animator.SetBool ("Running", false);
				break;
			case animations.running:
				animator.SetBool ("Running", true);
				break;
			case animations.climbing:
				break;

			default:
				break;
		}

	}

}
