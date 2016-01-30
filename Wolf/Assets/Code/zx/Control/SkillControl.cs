using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillControl : SingleTonGO<SkillControl>
{
	// for config
	public Dictionary<SkillType, SkillData> skillMap = new Dictionary<SkillType, SkillData> ();
	public GameObject attack;
	public List<GameObject> skill;
	public List<GameObject> sealSkill;
	public SkillData nowSkill;

	void Start ()
	{
		InitSkill ();
		InitEvent ();
	}

	void InitSkill ()
	{
		SkillData attackData = attack.GetComponent<SkillData> ();
		skillMap.Add (SkillType.Attack, attackData);

		for (int i =1; i<=4; i++) {
			GameObject temp = Utils.Instance.DeepFind (gameObject, "Skill" + i);
			SkillType stype = (SkillType)i;
			SkillData sdata = temp.GetComponent<SkillData> ();
			skillMap.Add (stype, sdata);
		}
		for (int i =11; i<=17; i++) {
			GameObject temp = Utils.Instance.DeepFind (gameObject, "SealSkill" + i);
			SkillType stype = (SkillType)i;
			SkillData sdata = temp.GetComponent<SkillData> ();
			skillMap.Add (stype, sdata);
		}
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
		SkillData data = skillMap [type];
		if (null != data) {
			if (data.canUse) {
				nowSkill = data;
				print ("UseSkill" + type);
			}
		}
	}

	void triggerSkill (RaycastHit hit)
	{
		// normal attack
		if (nowSkill == skillMap [SkillType.Attack]) {
			
		} else {
			// skill attack
			nowSkill = skillMap [SkillType.Attack];
		}
	}

	void RefreshSkillCDTime ()
	{
		foreach (KeyValuePair<SkillType,SkillData> cell in skillMap) {
			if (cell.Value.unlock) {
				if (cell.Value.cdTime > 0) {
					cell.Value.cdTime -= RealTime.deltaTime;
					cell.Value.cdTime = Mathf.Min (0, cell.Value.cdTime);
				}
			}
		}
	}

	void Update ()
	{
		RefreshSkillCDTime ();
	}
}
