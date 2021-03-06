﻿using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	private Rigidbody rigbi;
	private float startTimer;
	private float EndTimer;
	private bool already = false;
	
	void Start () {
		rigbi = GetComponent<Rigidbody> ();
	}
	

	void FixedUpdate () {

		if (AudioAnalyzer.Instance.getPitch() < 300f && AudioAnalyzer.Instance.getPitch() > 0f) {
			duck ();
		}

		if (AudioAnalyzer.Instance.getPitch() > 500f) {
			jump ();
		}

		if (Time.time - startTimer > 1f) {
			already = false;
			transform.localScale = new Vector3 (1f,1f,1f);
		}

		if (!already) {
			startTimer = Time.time;
		}

	}
	
	private void jump() {
		if (!already) {
			//transform.Translate (Vector3.up * 100 * Time.deltaTime, Space.World);
			rigbi.velocity += new Vector3(0f,5f,0f);
			already = true;
			startTimer = Time.time;
		}
	}

	private void duck() {
		if (!already) {
			transform.localScale = new Vector3 (1f, 0.5f, 1f);
			transform.position -= new Vector3 (0f, 0.5f, 0f);
			already = true;
			startTimer = Time.time;
		}
	}

	void Awake(){
		AudioAnalyzer.Instance.Init ();
	}
}
