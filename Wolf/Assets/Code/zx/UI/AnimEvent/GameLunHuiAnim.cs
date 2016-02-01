using UnityEngine;
using System.Collections;

public class GameLunHuiAnim : SingleTonGO<GameLunHuiAnim>
{
	public UILabel timeLabel;

	public void Play (Callback call)
	{
		GameProgress.Me.Lock ();

		PlanetControl.Me.PlayBackPlanet ();

		float catchYear = PlayerData.Me.year;

		timeLabel.gameObject.SetActive (true);
		LeanTween.cancel (gameObject);
		LeanTween.value (gameObject, f => {
			CameraControl.Me.autoSpeed = f;
			timeLabel.text = string.Format ("{0}万年后", (int)(75 * f / 10));
		}, 0.4f, 10, 3.5f).setOnComplete (() => {
			LeanTween.value (gameObject, f => {
				CameraControl.Me.autoSpeed = f;
				timeLabel.text = string.Format ("{0}万年后", (int)(750 + 75 * f / 10));
			}, 10, 0, 4).setOnComplete (() => {
				CameraControl.Me.autoSpeed = 0;
				
				if (call != null) {
					call ();
				}
				timeLabel.gameObject.SetActive (false);
			}).setUseEstimatedTime (true).setDestroyOnComplete (true);
		}).setUseEstimatedTime (true).setDestroyOnComplete (true);
	}
}
