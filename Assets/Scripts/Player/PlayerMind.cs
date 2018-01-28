using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMind : MonoBehaviour {
	public static PlayerMind Inst { get; private set; }

	public PossessableBase realBody;
	public PossessableBase currentBody;

	public ParticleSystem possessHighlightPrefab;
	public ParticleSystem transferParticles;

	public AudioClip transferSE;
	public AudioSource audioSource;

	public float clickRadius = 5f;
	public float transferTime = 1f;
	public float transferAtEndTime = 0.2f;

	public bool stopUpdates;
	
	private PossessableBase lastTargetBody;
	private ParticleSystem possessHighlight;
	private Coroutine transferDisplayCoroutine;

	// Use this for initialization
	void Start() {
		if (Inst) Debug.LogWarning("Two PlayerMinds in play!");
		Inst = this;

		possessHighlight = Instantiate(possessHighlightPrefab);
		if (!transferParticles) transferParticles = GetComponent<ParticleSystem>();
		if (!audioSource) audioSource = GetComponent<AudioSource>();

		Possess(realBody);
	}
	
	// Update is called once per frame
	void Update() {
		if (transferDisplayCoroutine == null) {
			transferParticles.transform.position = currentBody.transform.position;
		}

		if (stopUpdates) return;

		if (currentBody == realBody) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			PossessableBase target = PossessableBase.allActive
				.Where(p => p.CanBePossessed(this))
				.MinBy(clickRadius * clickRadius, (k) => ((Vector2)k.transform.position - mousePos).sqrMagnitude);

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
			stopUpdates = true;
			audioSource.PlayOneShot(transferSE);
			transferDisplayCoroutine = StartCoroutine(
				ShowTransfer(currentBody.transform, body.transform, transferTime, transferAtEndTime));
		} else {
			body.DoPossess(this);
		}
		currentBody = body;
		possessHighlight.Stop();
	}

	public void ReturnToBody() {
		if (currentBody != realBody) {
			Possess(realBody);
		}
	}

	IEnumerator ShowTransfer(Transform from, Transform to, float transferDuration, float waitAtEndDuration) {
		transferParticles.transform.position = from.position;
		transferParticles.Play();

		float startTime = Time.time;
		float endTime = startTime + transferDuration;
		yield return null;

		while (Time.time < endTime) {
			float t = Mathf.InverseLerp(startTime, endTime, Time.time);
			transferParticles.transform.position = Vector3.Lerp(from.position, to.position, t);
			yield return null;
		}
		
		currentBody.DoPossess(this);
		stopUpdates = false;
		if (currentBody != realBody) {
			transferDisplayCoroutine = null;
			yield break;
		}

		startTime = endTime;
		endTime = startTime + waitAtEndDuration;

		while (Time.time < endTime) {
			transferParticles.transform.position = to.position;
			yield return null;
		}

		transferParticles.Stop();
		transferDisplayCoroutine = null;
	}
}
