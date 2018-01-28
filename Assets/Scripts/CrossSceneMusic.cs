using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneMusic : MonoBehaviour {
	public static CrossSceneMusic Inst { get; private set; }

	public new AudioSource audio;

	void Awake () {
		if (!audio) audio = GetComponent<AudioSource>();

		if (Inst != null && Inst != this) {
			if (Inst.audio.clip != audio.clip) {
				Inst.audio.clip = audio.clip;
				Inst.audio.Play();
			}
			Destroy(gameObject);
			return;
		}

		Inst = this;
		DontDestroyOnLoad(gameObject);
	}

}
