using UnityEngine;
using System.Collections;

public class BgSound : SingleTonGO<BgSound>
{
	AudioSource aSource;

	void Start ()
	{
		aSource = GetComponent<AudioSource> ();
	}

	public void Play (AudioClip clip)
	{
		aSource.clip = clip;
		aSource.Play ();
	}
}
