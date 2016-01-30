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
	public bool triggerDragSkill = false;

	public SkillData DefaultSkill {
		get {
			return skillMap [SkillType.Attack];
		}
	}

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

		nowSkill = skillMap [SkillType.Attack];
	}
	
	void OnDestroy ()
	{
		RemoveEvent ();
	}
	
	void InitEvent ()
	{
		Messenger.AddListener<SkillType> (GameEventType.UseSkill, useSkill);
		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, normalAttack);
	}

	void RemoveEvent ()
	{
		Messenger.RemoveListener<SkillType> (GameEventType.UseSkill, useSkill);
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, normalAttack);
	}

	void useSkill (SkillType type)
	{
		SkillData data = skillMap [type];
		if (null != data) {
			if (data.canUse) {
				nowSkill = data;
				if (nowSkill.canUse) {
					if (nowSkill.triggertype == SkillTriggerType.Drag) {
						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit rayhit;
						if (Physics.Raycast (ray, out rayhit, 100)) {
							print ("Use Drag Skill" + nowSkill.type);
						}
					} else if (nowSkill.triggertype == SkillTriggerType.Click) {
						print ("Use Click Skill" + nowSkill.type);
					}
				}
				nowSkill.SetCD ();
				nowSkill = DefaultSkill;
			}
		}
	}

	void normalAttack (RaycastHit hit)
	{
		if (nowSkill == DefaultSkill && nowSkill.canUse) {
			if (null != hit.collider) {
				GameObject attack = Utils.Instance.LoadPfb ("Model/Normalattack");
				attack.transform.position = hit.point;
				attack.transform.up = hit.point.normalized;
				
				ClipSound.Me.Play ("dici_attack");
			}
			nowSkill.SetCD ();
		}
	}

	void RefreshSkillCDTime ()
	{
		foreach (KeyValuePair<SkillType,SkillData> cell in skillMap) {
			if (cell.Value.unlock) {
				if (cell.Value.nowcd > 0) {
					cell.Value.nowcd -= RealTime.deltaTime;
					cell.Value.nowcd = Mathf.Max (0, cell.Value.nowcd);
				}
			}
		}
	}

	void Update ()
	{
		RefreshSkillCDTime ();
	}
}
