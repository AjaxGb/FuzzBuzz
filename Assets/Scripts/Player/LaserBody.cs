using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBody : PossessableBase {

	public float offTime = 3;

	public Animator animator;

	public bool isOff;
	public float offEndTime;

	private int animKeyDeactivated;

	void Start() {
		if (!animator) animator = GetComponent<Animator>();

		animKeyDeactivated = Animator.StringToHash("Deactivated");
	}

	void Update() {
		if (isOff && Time.time > offEndTime) {
			isOff = false;
			animator.SetBool(animKeyDeactivated, false);
		}
	}

	protected override bool MindIsValid(PlayerMind mind) {
		return !isOff;
	}

	protected override void OnPossessed() {
		Possessor.ReturnToBody();
		isOff = true;
		animator.SetBool(animKeyDeactivated, true);
		offEndTime = Time.time + offTime;
	}

	public override void PossessedUpdate() { }
	protected override void OnUnpossessed() { }
}
