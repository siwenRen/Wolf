using UnityEngine;
using System.Collections;

public enum SkillType
{
	Attack = 0,
	// jishi jineng
	Skill1 = 1,
	Skill2 = 2,
	Skill3 = 3,
	Skill4 = 4,
	// fengyin jineng
	SealSkill1 = 11,
	SealSkill2 = 12,
	SealSkill3 = 13,
	SealSkill4 = 14,
	SealSkill5 = 15,
	SealSkill6 = 16,
	SealSkill7 = 17,
}

public class SkillData:MonoBehaviour
{
	public SkillType type;
	public string skillName;
	public bool unlock = false;
	public float cdTime;
}
