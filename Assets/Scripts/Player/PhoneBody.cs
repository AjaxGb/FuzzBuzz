using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBody : PossessableBase {

	protected override void OnPossessed() {
		Possessor.ReturnToBody();

		Debug.Log("Ring Ring");
	}

	public override void PossessedUpdate() {}
	protected override void OnUnpossessed() {}
}
