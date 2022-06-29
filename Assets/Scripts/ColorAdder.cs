using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAdder : ColorManager {
	public float power = 0.5f;
	public float uses = float.PositiveInfinity;
	public bool continuous = false;
	public Color colorToAdd
	{
		get {
			Color newColor = this.color;
			newColor.r *= this.power;
			newColor.g *= this.power;
			newColor.b *= this.power;
			return newColor;
		}
	}
	public AudioClip useClip, leaveClip;
	void Start () {
		if (this.useClip == null)
			this.useClip = GameController.singleton.defaultPickupSFX;
	}



	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			ColorManager manager = col.gameObject.GetComponent<ColorManager> ();
			if (!this.canAdd (manager)) {
				this.PlayClip (this.leaveClip);
				return;
			}
			this.PlayClip (this.useClip);
			manager.Add (colorToAdd);
			if (this.uses >= 0 || this.uses == float.PositiveInfinity) {
				this.uses--;
				if (this.uses <= 0) {
					this.rend.enabled = false;
					Collider tempCol = this.GetComponent<Collider>();
					if (tempCol != null)
						tempCol.enabled = false;
					Destroy (this.gameObject, this.PlayClip(this.useClip));
					}
				}
			}
		}

	private bool canAdd(ColorManager manager) {
		bool canAddR = this.colorToAdd.r > 0 && manager.red < 1;
		bool canAddG = this.colorToAdd.g > 0 && manager.green < 1;
		bool canAddB = this.colorToAdd.b > 0 && manager.blue < 1;
		return canAddR || canAddG || canAddB;
	}
	void OnTriggerStay(Collider col) {
		if (this.continuous && col.gameObject.tag == "Player") {
			ColorManager manager = col.gameObject.GetComponent<ColorManager> ();
			if (!this.canAdd (manager))
				return;
			Color c = new Color (colorToAdd.r * Time.deltaTime, colorToAdd.g * Time.deltaTime, colorToAdd.b * Time.deltaTime);
			manager.Add (c);
			if (this.uses >= 0 || this.uses == float.PositiveInfinity) {
				this.uses -= Time.deltaTime;
				if (this.uses <= 0)
					Destroy (this.gameObject);
			}
		}
	}
}
