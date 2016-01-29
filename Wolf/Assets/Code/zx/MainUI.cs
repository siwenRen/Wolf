using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainUI : MonoBehaviour
{
	private UISlider hpSlider;

	void Start ()
	{
		GameObject sliderObj = Utils.Instance.DeepFind (gameObject, "HpSlider");
		hpSlider = sliderObj.GetComponent<UISlider> ();

		InitButtons ();
	}

	void InitButtons ()
	{
		for (int i=0; i<4; i++) {
			GameObject tempButton = Utils.Instance.DeepFind (gameObject, "SkillButton" + i);
			UIEventListener.Get (tempButton).onClick = _clickSkillButton;
			UIButton button = tempButton.GetComponent<UIButton> ();
		}

		GameObject sealButton = Utils.Instance.DeepFind (gameObject, "SealButton");
		UIEventListener.Get (sealButton).onClick = _clickSealButton;
	}

	void _clickSkillButton (GameObject go)
	{
		print ("click skill" + go.name);
	}

	void _clickSealButton (GameObject go)
	{
		print ("click seal" + go.name);
	}

	void UpdateHpSlider ()
	{
		if (null != hpSlider) {
			if (hpSlider.value != ZhangYuData.Me.hpPct) {
				hpSlider.value = ZhangYuData.Me.hpPct;
			}
		}
	}

	void Update ()
	{
		UpdateHpSlider ();
	}
}
