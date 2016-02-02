using UnityEngine;
using System.Collections;

public class GameStartAnim : SingleTonGO<GameStartAnim>
{
	public Callback callback;

	void OnEnable ()
	{
		UITweener[] mTweens = GetComponentsInChildren<UITweener> ();
		foreach (var tween in mTweens) {
			tween.ResetToBeginning ();
		}
	}

	public void Play (Callback callback = null)
	{
		print ("GameStartAnim Play");
		gameObject.SetActive (true);

		// boss dengchang
		ZhangYuControl.Me.StartAnimator ();

		GameProgress.Me.Lock ();
		CameraControl.Me.ShakeCamera (0.02f, 3.5f);
		ClipSound.Me.Play ("BOSS_CX");

		LeanTween.cancel (gameObject);
		LeanTween.value (gameObject, (f) => {
			CameraControl.Me.autoSpeed = f;
		}, 0.4f, 0f, 3.5f).setUseEstimatedTime (true).setDestroyOnComplete (true).setOnComplete (() => {

			ClipSound.Me.Play ("BOSS_JCDM");
			CameraControl.Me.ShakeCamera (0.15f, 1);

			MainUI.Me.SliderIn (() => {
				GameProgress.Me.UnLock ();
				if (callback != null)
					callback ();
			});
		});
	}
}
