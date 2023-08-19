using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float moveSpeed = 20f;
	public float rotationSpeed = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * moveSpeed*ControlFreak2.CF2Input.GetAxis("Vertical")*Time.deltaTime);
		transform.Rotate (Vector3.up * rotationSpeed*ControlFreak2.CF2Input.GetAxis("Horizontal")*Time.deltaTime);
		
	}
}
