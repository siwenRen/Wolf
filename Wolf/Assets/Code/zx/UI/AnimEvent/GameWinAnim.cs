using UnityEngine;
using System.Collections;

public class GameWinAnim : SingleTonGO<GameWinAnim>
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

//				Time.timeScale = 1;
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

		endShow.SetActive (false);
		
		MainUI.Me.SliderOut (() => {
			// shegnli
			SkillFactory.Me.RandomBombWorid ();

			LeanTween.cancel (gameObject);
			LeanTween.value (gameObject, f => {
				CameraControl.Me.autoSpeed = f;
			}, 0, 0.4f, 1).setDestroyOnComplete (true).setUseEstimatedTime (true);

			LeanTween.delayedCall (gameObject, 10.0f, () => {
//				Time.timeScale = 0;
				endShow.SetActive (true);
				catchEvent = call;

				canExcuteClick = true;
			}).setDestroyOnComplete (true).setUseEstimatedTime (true);
		});
	}
}
