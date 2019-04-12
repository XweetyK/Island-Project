using UnityEngine;
using System.Collections;

public class PlayerMov : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	bool isRunning;
	bool isWalking;
	bool isSit;
	bool isCrouch;

	Animator animator;

	void Start () {
		isWalking = isRunning = isCrouch = isSit = false;
		animator = GetComponent<Animator> ();
	}

	void Update () {
		KeyInput ();
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}
			
		float targetSpeed = ((isRunning) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);

		UpdateAnimator ();
	}

	void KeyInput(){
		if (Input.GetButton ("Space")) {
			isRunning = true;
		} else {
			isRunning = false;
		}
		if (Input.GetButton ("Shift")) {
			isCrouch = true;
		} else {
			isCrouch = false;
		}
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			isWalking = true;
			isSit = false;
		} else {
			isWalking = false;
		}
		if (Input.GetButtonDown ("One")) {
			isSit = !isSit;
		}
	}

	void UpdateAnimator(){
		animator.SetBool ("Walking", isWalking);
		animator.SetBool ("Running", isRunning);
		animator.SetBool ("Crouch", isCrouch);
		animator.SetBool ("Sitting", isSit);
	}
}