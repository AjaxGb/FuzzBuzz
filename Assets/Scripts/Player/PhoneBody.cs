using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBody : PossessableBase {

	public float ringDelay = 1;
	public float distractDelay = 1;
	public float undistractDelay = 5;
	public AudioSource ringAudio;

	public GameObject[] undistractedStuff;
	public GameObject[] distractedStuff;

	public bool isDistracted;
	public float distractedEndTime;

	private Coroutine alertGuyRoutine;

	void Start() {
		if (!ringAudio) ringAudio = GetComponent<AudioSource>();
	}

	void Update() {
		if (isDistracted && Time.time > distractedEndTime) {
			isDistracted = false;

			foreach (GameObject go in undistractedStuff) {
				go.SetActive(true);
			}
			foreach (GameObject go in distractedStuff) {
				go.SetActive(false);
			}
		}
	}

	protected override void OnPossessed() {
		if (alertGuyRoutine == null) alertGuyRoutine = StartCoroutine(AlertGuy());

		Possessor.ReturnToBody();
	}

	private IEnumerator AlertGuy() {

		yield return new WaitForSeconds(ringDelay);

		ringAudio.Play();

		yield return new WaitForSeconds(distractDelay);

		foreach (GameObject go in undistractedStuff) {
			go.SetActive(false);
		}
		foreach (GameObject go in distractedStuff) {
			go.SetActive(true);
		}

		isDistracted = true;
		distractedEndTime = Time.time + undistractDelay;

		alertGuyRoutine = null;
	}

	public override void PossessedUpdate() {}
	protected override void OnUnpossessed() {}
}
