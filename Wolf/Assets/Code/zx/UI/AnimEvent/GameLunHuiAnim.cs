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
			timeLabel.text = string.Format ("{0}万年后", (int)(50 * f));
		}, 0.4f, 10, 3.5f).setOnComplete (() => {
			LeanTween.value (gameObject, f => {
				CameraControl.Me.autoSpeed = f;
				timeLabel.text = string.Format ("{0}万年后", (int)(500 + 50f * (10-f)));
			}, 10, 0, 4f).setOnComplete (() => {
				timeLabel.text = string.Format ("{0}万年后", (int)(1000));
				
				LeanTween.delayedCall(gameObject,3.0f, ()=>{

					CameraControl.Me.autoSpeed = 0;
					
					if (call != null) {
						call ();
					}
					timeLabel.gameObject.SetActive (false);

				}).setUseEstimatedTime (true).setDestroyOnComplete (true);

			}).setUseEstimatedTime (true).setDestroyOnComplete (true);
		}).setUseEstimatedTime (true).setDestroyOnComplete (true);
	}
}
