using UnityEngine;
using System.Collections;

public class LogoAnim : SingleTonGO<LogoAnim>
{
	public GameObject Tweens;
	public UITweener[] mTweens;
	public GameObject bgClick;
	bool excute;
	LTDescr delay;
	bool canExcute;
	Callback catchEvent;
	
	void Start ()
	{
		UIEventListener.Get (bgClick).onClick = (go) => {
			if (canExcute) {
				canExcute = false;
				
				GameObject word = Utils.Instance.DeepFind (gameObject, "Word");
				LeanTween.cancel (word);
				LeanTween.value (word, f => {
					word.GetComponent<UIWidget> ().alpha = f;
				}, 1, 0, 1.0f).setOnComplete (f => {
					UITweener ut = go.GetComponent<UITweener> ();
					ut.PlayReverse ();
					Tweens.SetActive (false);
					if (catchEvent != null) {
						catchEvent ();
					}
				}).setUseEstimatedTime (true).setDestroyOnComplete (true);
			}
		};
	}

	void Reset ()
	{
		foreach (UITweener tween in mTweens) {
			tween.ResetToBeginning ();
		}
	}

	public void Play (Callback call = null)
	{
		Tweens.SetActive (true);

		Reset ();

		foreach (UITweener tween in mTweens) {
			tween.PlayForward ();
		}

		mTweens [0].SetOnFinished (() => {
			catchEvent = call;
			canExcute = true;
		});
	}
}
