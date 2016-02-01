using UnityEngine;
using System.Collections;

public class BgSound : SingleTonGO<BgSound>
{
	public AudioClip bg1;
	public AudioClip bg2;

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

	public void PlayBg0()
	{
		Play (bg1);
	}

	public void PlayBg1()
	{
		Play (bg2);
	}
}
