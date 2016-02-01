using UnityEngine;
using System.Collections;

public class AnimUI : SingleTonGO<AnimUI>
{
	public UITweener sealAnim;
	public UISprite sealButton;
	public UISprite sealMask;
	UISprite sealSprite;

	void Start ()
	{
		InitEvent ();
		sealSprite = sealAnim.GetComponent<UISprite> ();
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener<SkillType> (GameEventType.SealBreak, playBeakAnim);
		Messenger.RemoveListener (GameEventType.RepairSeal, ChangeSealPic);
	}

	void InitEvent ()
	{
		Messenger.AddListener<SkillType> (GameEventType.SealBreak, playBeakAnim);
		Messenger.AddListener (GameEventType.RepairSeal, ChangeSealPic);
	}

	void playBeakAnim (SkillType stype)
	{
		Debug.LogWarning ("PlayBeakAnim" + stype);
		ClipSound.Me.Play ("fengyinkaiqi");
		switch (stype) {
		case SkillType.SealSkill1:
			sealSprite.spriteName = "1wenyi";
			break;
		case SkillType.SealSkill2:
			sealSprite.spriteName = "2tusha";
			break;
		case SkillType.SealSkill3:
			sealSprite.spriteName = "3jihuang";
			break;
		case SkillType.SealSkill4:
			sealSprite.spriteName = "4siwang";
			break;
		case SkillType.SealSkill5:
			sealSprite.spriteName = "5yunshi";
			break;
		case SkillType.SealSkill6:
			sealSprite.spriteName = "6heian";
			break;
		case SkillType.SealSkill7:
			sealSprite.spriteName = "7jijing";
			break;
		}
		sealSprite.MakePixelPerfect ();
		sealButton.MakePixelPerfect ();
		sealMask.MakePixelPerfect ();

		sealAnim.ResetToBeginning ();
		sealAnim.PlayForward ();
	}

	void ChangeSealPic ()
	{
		int nowState = SealData.Me.nowBreakState;
		sealButton.spriteName = (nowState * 2 + 1).ToString ();
		sealMask.spriteName = (nowState * 2 + 2).ToString ();

		sealButton.MakePixelPerfect ();
		sealMask.MakePixelPerfect ();
	}

	void Reset ()
	{

	}
}
