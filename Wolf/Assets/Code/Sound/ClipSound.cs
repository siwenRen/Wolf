using UnityEngine;
using System.Collections;

public class ClipSound : SingleTonGO<ClipSound>
{
	UIPlaySound ups;

	void Start ()
	{
		ups = GetComponent<UIPlaySound> ();
	}

	public void Play (AudioClip clip = null)
	{
		if (null != clip) {
			ups.audioClip = clip;
		}
		ups.Play ();
	}
}
