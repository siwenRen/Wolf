using UnityEngine;
using System.Collections;

public enum SkillType
{
	Attack,
	Skill1,
	Skill2,
	Skill3,
	Skill4,
}

public class SkillData
{
	public class SkillCellData
	{
		public SkillType type;
		public bool unlock = false;
		public float cdTime;
	}

	static SkillData me;

	public static SkillData Me {
		get {
			if (null == me) {
				me = new SkillData ();
			}
			return me;
		}
	}

	public SkillType nowSkill = SkillType.Attack;
}
