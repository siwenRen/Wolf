using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainUI : SingleTonGO<MainUI>
{
	public UIPanel panel;
	public UILabel yearlabel;
	public UISlider hpslider;
	public FollowCurse followCurse;
	public GameObject Container_Left;
	public GameObject Container_Right;
	private UISprite sealMask;
	Dictionary<int, UISprite> skillMasks = new Dictionary<int, UISprite> ();

	void Start ()
	{
		InitButtons ();
	}

	void InitButtons ()
	{
		for (int i=1; i<5; i++) {
			GameObject tempButton = Utils.Instance.DeepFind (gameObject, "SkillButton" + i);
			UIEventListener.Get (tempButton).onClick = _clickSkillButton;
			UIEventListener.Get (tempButton).onPress = _dragSkillButton;
			UIButton button = tempButton.GetComponent<UIButton> ();

			GameObject mk = Utils.Instance.DeepFind (tempButton, "Mask");
			skillMasks.Add (i, mk.GetComponent<UISprite> ());
		}

		GameObject sealButton = Utils.Instance.DeepFind (gameObject, "SealButton");
		GameObject obj = Utils.Instance.DeepFind (gameObject, "SealMask");
		sealMask = obj.GetComponent<UISprite> ();
		UIEventListener.Get (sealButton).onPress = _dragSealButton;
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
			followCurse.open = state;
			if (state) {

			} else {
				Messenger.Broadcast (GameEventType.UseSkill, stype);
			}
		}
	}

	void _dragSealButton (GameObject go, bool state)
	{
		if (SealData.Me.sealPct >= 1) {
			SkillType stype = SealData.Me.GetSealSkillType ();
			CheckDragSkill (stype, go, state);
		}
	}

	void UpdateHpSlider ()
	{
		if (null != hpslider) {
			if (hpslider.value != ZhangYuData.Me.hpPct) {
				hpslider.value = ZhangYuData.Me.hpPct;
			}
		}
	}

	void UpdateMask ()
	{
		if (sealMask && SealData.Me) {
			sealMask.fillAmount = 1 - SealData.Me.sealPct;
		}
		foreach (KeyValuePair<int, UISprite> cell in skillMasks) {
			SkillData sd = SkillControl.Me.skillMap [(SkillType)cell.Key];
			cell.Value.fillAmount = sd.nowcd * 1.0f / sd.cdTime;
		}
	}

	void UpdateTime ()
	{
//		yearlabel.text = string.Format ("{0}万年后", (int)PlayerData.Me.year);
	}

	void Update ()
	{
		UpdateHpSlider ();
		UpdateMask ();
		UpdateTime ();
	}

	public void SliderOut (Callback callback)
	{
		LeanTween.cancel (Container_Left);
		LeanTween.moveLocalX (Container_Left, -1000, 0.5f).setOnComplete (() => {
			if (callback != null) {
				callback ();
			}
		}).setUseEstimatedTime (true);
		
		LeanTween.cancel (Container_Right);
		LeanTween.moveLocalX (Container_Right, 1000, 0.5f).setUseEstimatedTime (true);
	}

	public void SliderIn (Callback callback)
	{
		LeanTween.cancel (Container_Left);
		LeanTween.moveLocalX (Container_Left, -640, 0.5f).setOnComplete (() => {
			if (callback != null) {
				callback ();
			}
		}).setUseEstimatedTime (true);

		LeanTween.cancel (Container_Right);
		LeanTween.moveLocalX (Container_Right, 640, 0.5f).setUseEstimatedTime (true);
	}
}
