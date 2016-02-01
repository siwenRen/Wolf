using UnityEngine;
using System.Collections;

public class WaittingStartAnim : SingleTonGO<WaittingStartAnim>
{
	public GameObject Tweens;
	public UITweener bgTween;
	public GameObject clickGO;
	bool canExcuteClick;
	Callback catchEvent;

	void Start ()
	{
		UIEventListener.Get (clickGO).onClick = (go) => {
			if (canExcuteClick) {
				canExcuteClick = false;

				LeanTween.cancel (gameObject);
				LeanTween.value (gameObject, (f) => {
					bgTween.GetComponent<UIWidget> ().alpha = f;
					CameraControl.Me.autoSpeed = f * 0.4f;
				}, 1, 0, 1.0f).setOnComplete (() => {
					CameraControl.Me.autoSpeed = 0;
					
					if (catchEvent != null) {
						catchEvent ();
					}
					Tweens.SetActive (false);
				}).setUseEstimatedTime (true).setDestroyOnComplete (true);
			}
		};
	}
	
	public void Play (Callback callback)
	{
		Tweens.SetActive (true);

		bgTween.ResetToBeginning ();

		CameraControl.Me.autoSpeed = 0.4f;

		bgTween.PlayForward ();
		bgTween.SetOnFinished (() => {
			catchEvent = callback;
			canExcuteClick = true;
		});
	}
}
