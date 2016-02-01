using UnityEngine;
using System.Collections;

public enum SkillType
{
	Attack = 0,
	// jishi jineng
	Skill1 = 1,	//area damage
	Skill2 = 2,	//stop enemy factory
	Skill3 = 3,	//black hole
	Skill4 = 4,	//slow down
	// fengyin jineng
	SealSkill1 = 11,
	SealSkill2 = 12,
	SealSkill3 = 13,
	SealSkill4 = 14,
	SealSkill5 = 15,
	SealSkill6 = 16,
	SealSkill7 = 17,
}

public enum SkillTriggerType
{
	RayCast,
	Click,
	Drag,
}

public class SkillData:MonoBehaviour
{
	public SkillType type;
	public string skillName;
	public string skillDesc;
	public GameObject effect;
	public bool unlock = false;
	public float cdTime;
	public float nowcd = 0;
	public SkillTriggerType triggertype;

	public bool canUse {
		get {
			return unlock && nowcd <= 0;
		}
	}

	public void SetCD ()
	{
		nowcd = cdTime;
	}
}
