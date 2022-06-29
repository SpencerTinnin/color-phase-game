using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {
	public static Color playerColor;
	public Slider redSlider, greenSlider, blueSlider;
	public Text redMeterText, blueMeterText, greenMeterText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		redSlider.value = playerColor.r;

		greenSlider.value = playerColor.g;
		blueSlider.value = playerColor.b;

		this.redMeterText.text = (Mathf.RoundToInt(redSlider.value * 5)).ToString() + "/5";
		this.greenMeterText.text = (Mathf.RoundToInt(greenSlider.value * 5)).ToString() + "/5";
		this.blueMeterText.text = (Mathf.RoundToInt(blueSlider.value * 5)).ToString() + "/5";
	}
}
