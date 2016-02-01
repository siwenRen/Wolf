using UnityEngine;
using System.Collections;

public class SkyBall : SingleTonGO<SkyBall>
{
	Vector3 catchPos;

	public void ShakeSkyBall (float strengths = 1.0f, float time = 1.0f)
	{
		catchPos = transform.position;
		LeanTween.cancel (gameObject);
		LeanTween.value (gameObject, (f) => {
			transform.position = catchPos + Random.insideUnitSphere * strengths * 10;
		}, 0, 1, time).setOnComplete (g => {
			transform.position = catchPos;
		}).setDestroyOnComplete (true).setUseEstimatedTime (true);
	}
}
