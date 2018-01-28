using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBody : PossessableBase {

	public float ringDelay = 1;
	public float distractDelay = 1;
	public float undistractDelay = 5;
	public AudioSource ringAudio;

	public Animator phoneAnimator;
	public Animator distractedAnimator;
	public string distractedKey = "Distracted";

	public bool isDistracted;
	public float distractedEndTime;

	private Coroutine alertGuyRoutine;
	private int animKeyRing;

	void Start() {
		if (!ringAudio) ringAudio = GetComponent<AudioSource>();
		if (!phoneAnimator) phoneAnimator = GetComponent<Animator>();

		animKeyRing = Animator.StringToHash("Ring");
	}

	void Update() {
		if (isDistracted && Time.time > distractedEndTime) {
			isDistracted = false;

			distractedAnimator.SetBool(distractedKey, false);
		}
	}

	protected override void OnPossessed() {
		if (alertGuyRoutine == null) alertGuyRoutine = StartCoroutine(AlertGuy());

		Possessor.ReturnToBody();
	}

	private IEnumerator AlertGuy() {

		yield return new WaitForSeconds(ringDelay);

		phoneAnimator.SetTrigger(animKeyRing);
		ringAudio.Play();

		yield return new WaitForSeconds(distractDelay);

		isDistracted = true;
		distractedAnimator.SetBool(distractedKey, false);
		distractedAnimator.SetBool(distractedKey, true);
		distractedEndTime = Time.time + undistractDelay;

		alertGuyRoutine = null;
	}

	public override void PossessedUpdate() {}
	protected override void OnUnpossessed() {}
}
