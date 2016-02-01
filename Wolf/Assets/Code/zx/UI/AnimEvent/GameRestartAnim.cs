using UnityEngine;
using System.Collections;

public class GameRestartAnim : SingleTonGO<GameRestartAnim>
{
	public Callback callback;
	
	public void Play ()
	{
		if (null != callback) {
			callback ();
		}
	}
}
