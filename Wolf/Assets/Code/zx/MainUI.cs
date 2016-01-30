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
		for (int i=1; i<5; i++) {
			GameObject tempButton = Utils.Instance.DeepFind (gameObject, "SkillButton" + i);
			UIEventListener.Get (tempButton).onClick = _clickSkillButton;
			UIEventListener.Get (tempButton).onPress = _dragSkillButton;
			UIButton button = tempButton.GetComponent<UIButton> ();
		}

		GameObject sealButton = Utils.Instance.DeepFind (gameObject, "SealButton");
		UIEventListener.Get (sealButton).onClick = _clickSealButton;
	}

	void _clickSkillButton (GameObject go)
	{
		int index = System.Convert.ToInt32 (go.name.Substring ("SkillButton".Length));
		SkillType stype = (SkillType)(index);
		CheckClickSkill (stype, go);
	}

	void _dragSkillButton (GameObject go, bool state)
	{
		int index = System.Convert.ToInt32 (go.name.Substring ("SkillButton".Length));
		SkillType stype = (SkillType)(index);
		CheckDragSkill (stype, go, state);
	}
	
	void CheckClickSkill (SkillType stype, GameObject go)
	{
		SkillData sdata = SkillControl.Me.skillMap [stype];
		if (sdata.triggertype == SkillTriggerType.Click) {
			Messenger.Broadcast (GameEventType.UseSkill, stype);
		}
	}

	void CheckDragSkill (SkillType stype, GameObject go, bool state)
	{
		SkillData sdata = SkillControl.Me.skillMap [stype];
		if (sdata.canUse && sdata.triggertype == SkillTriggerType.Drag) {
			if (state) {
				
			} else {
				Messenger.Broadcast (GameEventType.UseSkill, stype);
			}
		}
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
