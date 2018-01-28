using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMind : MonoBehaviour {
	public static PlayerMind Inst { get; private set; }

	public PossessableBase realBody;
	public PossessableBase currentBody;

	public ParticleSystem possessHighlightPrefab;
	public ParticleSystem transferHighlightPrefab;

	public float transferTime = 1f;
	public float transferAtEndTime = 0.2f;
	
	private PossessableBase lastTargetBody;

	private ParticleSystem possessHighlight;
	private ParticleSystem transferHighlight;

	private Coroutine transferDisplayCoroutine;

	public bool stopUpdates;

	// Use this for initialization
	void Start() {
		if (Inst) Debug.LogWarning("Two PlayerMinds in play!");
		Inst = this;

		possessHighlight = Instantiate(possessHighlightPrefab);
		transferHighlight = Instantiate(transferHighlightPrefab);
		Possess(realBody);
	}
	
	// Update is called once per frame
	void Update() {
		if (transferDisplayCoroutine == null) {
			transferHighlight.transform.position = currentBody.transform.position;
		}

		if (stopUpdates) return;

		if (currentBody == realBody) {
			PossessableBase target = PossessableBase.allActive
				.Where(p => p.CanBePossessed(this))
				.MinBy((k) => Vector3.Distance(k.transform.position, transform.position));

			if (target) {
				if (target != lastTargetBody) possessHighlight.Pause();
				possessHighlight.transform.position = target.transform.position;
				if (!possessHighlight.isPlaying) possessHighlight.Play();

				if (Input.GetButtonDown("Possess")) {
					Possess(target);
				}
			} else {
				possessHighlight.Stop();
			}

			lastTargetBody = target;
		} else {
			if (Input.GetButtonDown("Possess")) {
				Possess(realBody);
			}
		}

		currentBody.PossessedUpdate();
	}

	public void Possess(PossessableBase body) {
		if (transferDisplayCoroutine != null) {
			StopCoroutine(transferDisplayCoroutine);
			transferDisplayCoroutine = null;
		}
		if (currentBody) {
			currentBody.DoUnpossess();
			transferDisplayCoroutine = StartCoroutine(
				ShowTransfer(currentBody.transform, body.transform, transferTime, transferAtEndTime));
		}
		currentBody = body;
		currentBody.DoPossess(this);
		possessHighlight.Stop();
	}

	public void ReturnToBody() {
		if (currentBody != realBody) {
			Possess(realBody);
		}
	}

	IEnumerator ShowTransfer(Transform from, Transform to, float transferDuration, float waitAtEndDuration) {
		transferHighlight.transform.position = from.position;
		transferHighlight.Play();

		float startTime = Time.time;
		float endTime = startTime + transferDuration;
		yield return null;

		while (Time.time < endTime) {
			float t = Mathf.InverseLerp(startTime, endTime, Time.time);
			transferHighlight.transform.position = Vector3.Lerp(from.position, to.position, t);
			yield return null;
		}

		if (currentBody != realBody) {
			transferDisplayCoroutine = null;
			yield break;
		}

		startTime = endTime;
		endTime = startTime + waitAtEndDuration;

		while (Time.time < endTime) {
			transferHighlight.transform.position = to.position;
			yield return null;
		}

		transferHighlight.Stop();
		transferDisplayCoroutine = null;
	}
}
