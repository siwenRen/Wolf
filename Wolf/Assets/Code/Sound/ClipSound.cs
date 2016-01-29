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

	public void Play (string name)
	{
		AudioClip ac = Resources.Load ("Sound/" + name) as AudioClip;
		if (null != ac) {
			ups.audioClip = ac;
		}
		ups.Play ();
	}
}
