using UnityEngine;
using System.Collections;

public class GameEndAnim : SingleTonGO<GameEndAnim>
{
	public GameObject Tweens;
	public GameObject clickGO;
	public GameObject endShow;
	bool canExcuteClick;
	Callback catchEvent;

	void Start ()
	{
		UIEventListener.Get (clickGO).onClick = (go) => {
			if (canExcuteClick) {
				canExcuteClick = false;

				Time.timeScale = 1;
				endShow.SetActive (false);
				Tweens.SetActive (false);
				
				if (catchEvent != null) {
					catchEvent ();
					catchEvent = null;
				}
			}
		};
	}

	public void Play (Callback call)
	{
		Tweens.SetActive (true);

		GameProgress.Me.Lock ();

		MainUI.Me.SliderOut (() => {
			ZhangYuControl.Me.Die ();
			LeanTween.cancel (gameObject);
			LeanTween.delayedCall (gameObject, 3.0f, () => {
				Time.timeScale = 0;
				endShow.SetActive (true);
				catchEvent = call;

				canExcuteClick = true;
			});
		});
	}
}
