using UnityEngine;
using System.Collections;

public class SkillData
{
	static SkillData me;

	public static SkillData Me {
		get {
			if (null == me) {
				me = new SkillData ();
			}
			return me;
		}
	}

	public enum SkillType
	{
		Attack,
		Skill1,
		Skill2,
		Skill3,
		Skill4,
	}

	public SkillType nowSkill = SkillType.Attack;
}
