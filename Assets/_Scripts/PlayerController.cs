using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float inputX, inputY;
	public float playerSpeed;
	bool isWalking;
	Animator animPlayer;
	bool joystickConnected;
	Rigidbody rb;

	public Vector3 leftRight;
	public Vector3 topBottom;

	// Use this for initialization
	void Start () {
		topBottom = Camera.main.transform.forward;
		topBottom.y = 0f;
		topBottom.Normalize();
		rb = GetComponent<Rigidbody>();

		leftRight = Camera.main.transform.right;

		animPlayer = GetComponent<Animator>();
		string[] joysticks = Input.GetJoystickNames();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleKeyboardMovement();

	}

	void HandleJoystickMovement(float horizontal, float vertical) {

	}

	void HandleKeyboardMovement() {
		if (Input.GetKey(KeyCode.A)) {
			//transform.position += -leftRight * Time.deltaTime * playerSpeed;
			rb.AddForce(-leftRight * playerSpeed * Time.deltaTime, ForceMode.VelocityChange);
		} else if (Input.GetKey(KeyCode.D)) {
			//transform.position += leftRight * Time.deltaTime * playerSpeed;
			rb.AddForce(leftRight * playerSpeed * Time.deltaTime, ForceMode.VelocityChange);
		}

		if (Input.GetKey(KeyCode.W)) {
			//transform.position += topBottom * Time.deltaTime * playerSpeed;
			rb.AddForce(topBottom * playerSpeed * Time.deltaTime, ForceMode.VelocityChange);
		} else if (Input.GetKey(KeyCode.S)) {
			//transform.position += -topBottom * Time.deltaTime * playerSpeed;
			rb.AddForce(-topBottom * playerSpeed * Time.deltaTime, ForceMode.VelocityChange);
		}
	}

}
