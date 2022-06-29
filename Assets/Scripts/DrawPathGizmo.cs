using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathGizmo : MonoBehaviour {

	public Transform[] path;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		for (int i = 1; i < this.path.Length; i++) {
			Gizmos.DrawLine (this.path [i - 1].position, this.path [i].position);
		}
	}
}
