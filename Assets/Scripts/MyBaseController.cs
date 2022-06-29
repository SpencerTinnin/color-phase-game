using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBaseController : MonoBehaviour {
	[SerializeField]
	new protected AudioSource audio;
	[SerializeField]
	new protected Collider collider;
	[SerializeField]
	protected Renderer rend;
	protected Material mat
	{
		get
		{
			if (this.rend == null)
				return null;
			else
				return this.rend.material;
		}
		set {
			if (this.rend != null)
				this.rend.material = value;
		}
	}
	protected Color matColor
	{
		get {
			if (this.mat == null)
				return Color.black;
			else
				return this.mat.color;
		}
		set {
			Material newMat = this.mat;
			if (newMat != null) {
				newMat.color = value;
				this.mat = newMat;
			}
		}
	}

	// Use this for initialization
	void Start () {

		if (this.audio == null) {
			this.audio = this.gameObject.GetComponent<AudioSource> ();
			if (this.audio == null) {
				AudioSource newSource = this.gameObject.AddComponent<AudioSource> ();
				newSource.playOnAwake = false;
				newSource.loop = false;
				

			}
		}
		if ((this.collider == null))
			this.collider = this.gameObject.GetComponent<Collider> ();
		if (this.rend == null)
			this.rend = this.gameObject.GetComponent<Renderer> ();
	}
	
	protected float PlayClip(AudioClip clipToPlay)
	{
		if (this.audio == null || clipToPlay == null)
			return 0.0f;
		this.audio.clip = clipToPlay;
		this.audio.Play ();
		return clipToPlay.length;
	}
}
