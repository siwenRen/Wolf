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

	void Start ()
	{
		InitSkill ();
		InitEvent ();
	}

	void InitSkill ()
	{
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
		print ("UseSkill" + type);
	}

	void triggerSkill (RaycastHit hit)
	{

	}

	void RefreshSkillCDTime ()
	{

	}

	void Update ()
	{
		RefreshSkillCDTime ();
	}
}
