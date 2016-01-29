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
		Messenger.AddListener (GameEventType.TriggerAttack, triggerSkill);
	}
	
	void RemoveEvent ()
	{
		Messenger.RemoveListener (GameEventType.TriggerAttack, triggerSkill);
	}

	void triggerSkill ()
	{
		if (SkillData.Me.nowSkill != SkillData.SkillType.Attack) {
			SkillData.Me.nowSkill = SkillData.SkillType.Attack;
		} else {
			print (SkillData.Me.nowSkill);
		}
	}
}
