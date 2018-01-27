using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PossessableBase : MonoBehaviour {
	public PlayerMind Possessor { get; private set; }
	public bool IsPossessed { get { return Possessor; } }

	public static HashSet<PossessableBase> allActive = new HashSet<PossessableBase>();

	void OnEnable() {
		allActive.Add(this);
	}

	void OnDisable() {
		allActive.Remove(this);
	}

	public bool CanBePossessed(PlayerMind mind) {
		return !IsPossessed && MindIsValid(mind);
	}

	public void DoPossess(PlayerMind mind) {
		Possessor = mind;
		OnPossessed();
	}

	public void DoUnpossess() {
		Possessor = null;
		OnUnpossessed();
	}

	protected virtual bool MindIsValid(PlayerMind mind) {
		return true;
	}

	protected abstract void OnPossessed();
	protected abstract void OnUnpossessed();
	public abstract void PossessedUpdate();
}
