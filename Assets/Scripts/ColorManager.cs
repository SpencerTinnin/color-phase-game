using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MyBaseController {
	
	[SerializeField] private float _red = 0.0f, _green = 0.0f, _blue = 0.0f;
	public float red
	{
		get
		{
			return this._red;
		}
		set {
			this._red = Mathf.Clamp01 (value);
		}
	}
	public float green
	{
		get
		{
			return this._green;
		}
		set {
			this._green = Mathf.Clamp01 (value);
		}
	}
	public float blue
	{
		get
		{
			return this._blue;
		}
		set {
			this._blue = Mathf.Clamp01 (value);
		}
	}
	public Color color
	{
		get {
			return new Color (this.red, this.green, this.blue);
		}
	}



	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.rend != null)
			updateColor ();
	}

	public void updateColor()
	{
		Color newColor = new Color (this.red, this.green, this.blue, this.rend.material.color.a);
		Material newMaterial = this.rend.material;
		newMaterial.color = newColor;

		if (newMaterial.HasProperty("_Emission")) {
			float newH, newS, newV, emissionH, emissionS, emissionV;
			Color.RGBToHSV (newColor, out newH, out newS, out newV);
			Color.RGBToHSV(newMaterial.GetColor("_Emission"), out emissionH, out emissionS, out emissionV);

			Color newEmissionColor = Color.HSVToRGB (newH, newS, emissionV);
			newMaterial.SetColor ("_Emission", newEmissionColor);
			this.mat = newMaterial;
		}
			
		if (this.gameObject.tag == "Player")
			UI_Controller.playerColor = newColor;
	}

	public void Add(ColorManager cm) {
		this.Add (new Color(cm.red, cm.green, cm.blue));
	}
	public void Add(Color c) {
		this.red = this.red + c.r;
		this.green = this.green + c.g;
		this.blue = this.blue + c.b;

	}

	public void Subtract(ColorManager cm) {
		this.Subtract (new Color(cm.red, cm.green, cm.blue));
	}
	public void Subtract(Color c) {
		this.red = this.red - c.r;
		this.green = this.green - c.g;
		this.blue = this.blue - c.b;
	}


}
