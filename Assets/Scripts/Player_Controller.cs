using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
	public Animator playerAnimator;
	public float currentSpeed = 0.0f;
	public float maxSpeed = 100.0f;
	public float rotationSpeed = 90.0f;
	private float strength
	{
		get {
			return (this.cm.red + this.cm.green + this.cm.blue) / 3.0f;
		}
	}
	private ColorManager cm
	{
		get
		{ return this.GetComponent<ColorManager> (); }
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.singleton == null || !GameController.singleton.isPaused)
			this.HandleMovement ();
	}

	void HandleMovement() {
		float walkSpeed = Mathf.Clamp (this.strength, 0.5f, 1.0f) * this.maxSpeed;
		//Move Forewards/Backwards
		this.currentSpeed = Input.GetAxis ("Vertical") * walkSpeed * (Input.GetAxis("Sprint") + 1);
		this.GetComponent<Rigidbody>().velocity = this.currentSpeed * this.transform.forward;
		this.playerAnimator.SetFloat ("Speed", Mathf.Abs(this.currentSpeed));

		//Turn left/right
		Quaternion currentRotation = this.gameObject.transform.rotation;
		Vector3 newRotationEuler = currentRotation.eulerAngles;
		newRotationEuler.y += Input.GetAxis ("Horizontal") * this.rotationSpeed * Time.deltaTime;

		Quaternion newRotation = Quaternion.Euler (newRotationEuler);

		this.gameObject.transform.rotation = newRotation;

	}


}
