using UnityEngine;
using System.Collections;

public class SkillControl : SingleTonGO<SkillControl>
{
	void Start ()
	{
		InitEvent ();
	}
	
	void OnDestroy ()
	{
		RemoveEvent ();
	}
	
	void InitEvent ()
	{
		Messenger.AddListener<SkillType> (GameEventType.UseSkill, useSkill);
		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, triggerSkill);
	}
	
	void RemoveEvent ()
	{
		Messenger.RemoveListener<SkillType> (GameEventType.UseSkill, useSkill);
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, triggerSkill);
	}

	void useSkill (SkillType type)
	{
		print ("UseSkill" + type);
	}

	void triggerSkill (RaycastHit hit)
	{
		if (SkillData.Me.nowSkill != SkillType.Attack) {
			SkillData.Me.nowSkill = SkillType.Attack;
		} else {

		}
	}

	void RefreshSkillCDTime ()
	{

	}

	void Update ()
	{
		RefreshSkillCDTime ();
	}
}
