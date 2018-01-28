using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public Animator pressedAnimation;

	private int animKeyPressed;
	private HashSet<Collider2D> allTouching = new HashSet<Collider2D>();

	void Awake() {
		animKeyPressed = Animator.StringToHash("Pressed");
	}

	void OnTriggerEnter2D(Collider2D other) {
		allTouching.Add(other);
		CheckPressed();
	}

	void OnTriggerExit2D(Collider2D other) {
		allTouching.Remove(other);
		CheckPressed();
	}

	public void CheckPressed() {
		pressedAnimation.SetBool(animKeyPressed, allTouching.Count > 0);
	}
}
