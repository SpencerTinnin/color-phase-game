using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrierController : ColorManager {
	public float power = 0.2f;
	public bool oneTime = false;
	private bool open = false;

	public Color colorToSubtract
	{
		get {
			Color newColor = this.color;
			newColor.r *= this.power;
			newColor.g *= this.power;
			newColor.b *= this.power;
			return newColor;
		}
	}
	public AudioClip successClip, failureClip, unlockClip;
	// Use this for initialization
	void Start () {
		
		if (this.successClip == null)
			this.successClip = GameController.singleton.defaultBarrierSuccessSFX;
		if (this.failureClip == null)
			this.failureClip = GameController.singleton.defaultBarrierFailSFX;
		if (this.unlockClip == null)
			this.unlockClip = GameController.singleton.defaultBarrierUnlockSFX;
	}

	private bool CanPass(ColorManager manager) {
		bool hasRed = manager.red >= this.colorToSubtract.r;
		bool hasGreen = manager.green >= this.colorToSubtract.g;
		bool hasBlue = manager.blue >= this.colorToSubtract.b;
		return hasRed && hasGreen && hasBlue;
	}
	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.tag == "Player" && (!this.oneTime || !this.open)) {
			ColorManager manager = col.collider.gameObject.GetComponent<ColorManager> ();
			if (this.CanPass (manager)) {
				manager.Subtract (colorToSubtract);
				if (this.oneTime) {
					this.rend.enabled = false;
					this.collider.enabled = false;
					Destroy (this.gameObject, this.PlayClip (this.unlockClip));
				} else {	
					this.PlayClip (this.successClip);
					this.collider.isTrigger = true;
				}
			}
			else
				this.PlayClip (this.failureClip);
		} 
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Player")
			this.collider.isTrigger = false;
	}


}
