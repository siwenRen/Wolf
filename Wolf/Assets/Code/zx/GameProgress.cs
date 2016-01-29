using UnityEngine;
using System.Collections;

public class GameProgress : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);

		GameStart ();
	}

	void GameStart ()
	{

	}

	void OnDestroy ()
	{
		Messenger.Cleanup ();
	}
}
