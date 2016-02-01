using UnityEngine;
using System.Collections;

public class SkillFactory : SingleTonGO<SkillFactory>
{
	void Start ()
	{
		Messenger.AddListener<SkillData,RaycastHit> (GameEventType.TriggerDragSkill, TriggerDragSkill);
		Messenger.AddListener<SkillData> (GameEventType.TriggerClickSkill, TriggerClickSkill);
//		Messenger.AddListener(GameEventType.GameWin, RandomBombWorid);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener<SkillData,RaycastHit> (GameEventType.TriggerDragSkill, TriggerDragSkill);
		Messenger.RemoveListener<SkillData> (GameEventType.TriggerClickSkill, TriggerClickSkill);
//		Messenger.RemoveListener(GameEventType.GameWin, RandomBombWorid);
	}

	public void TriggerDragSkill (SkillData data, RaycastHit hit)
	{
		GameObject obj = CreateSkill (data);
		if (obj != null){ //&& hit.collider.gameObject.layer == LayerMask.NameToLayer("SPlanet")) {
			obj.transform.position = hit.point;
		}
	}

	public void TriggerClickSkill (SkillData data)
	{
		GameObject obj = CreateSkill (data);
		if (obj != null) {
			obj.transform.position = Vector3.zero;
		}

		if (data.type == SkillType.Skill4) {
			Messenger.Broadcast (GameEventType.SkillSlowDown);
		}
	}

	public GameObject CreateSkill (SkillData data)
	{
		if (data == null)
			data = SkillControl.Me.DefaultSkill;

		GameObject obj = null;
		if (data.effect != null) {
			obj = Instantiate (data.effect) as GameObject;
		}

		return obj;
	}

	public void RandomBombWorid ()
	{
		ZhangYuControl.Me.EndAnimator ();
		gameObject.AddComponent<BombWorid> ();
	}
}
