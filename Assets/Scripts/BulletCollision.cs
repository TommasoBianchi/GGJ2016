﻿using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Spider") {
			other.gameObject.GetComponent<Spider> ().Hit (1);
			Destroy (gameObject);
		}
	}
}