using UnityEngine;
using System.Collections;
using System;

public class BanKuaiControl : MonoBehaviour
{
	public enum BanKuaiState
	{
		S1 = 15, 	//  1帧循环  好板块
		S2 = 12, 	//  过程    好板块破掉
		S3 = 10, 	//  1帧循环  坏板块
		S4 = 8, 	//  过程     大破
		S5 = 6, 	//  1帧循环   大破
		S6 = 2, 	//  过程     恢复
	}

	public BanKuaiData data;
	public SimpleAnimatorPlayer simpanim;

	private Animator	mAnimator;
	private GameObject	mSealEffect;

	void Start ()
	{
		if (null == data) {
			data = Utils.Instance.GetOrAddComponent<BanKuaiData> (gameObject);
		}
		simpanim = Utils.Instance.GetOrAddComponent < SimpleAnimatorPlayer> (gameObject);

		mAnimator = GetComponent<Animator> ();

		InitEvent ();
	}
	
	void OnDestroy ()
	{
		RemoveEvent ();
	}
	
	void InitEvent ()
	{
		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickBanKuai);
		Messenger.AddListener<SkillData,RaycastHit> (GameEventType.TriggerDragSkill, DragBanKuai);
	}
	
	void RemoveEvent ()
	{
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickBanKuai);
		Messenger.RemoveListener<SkillData,RaycastHit> (GameEventType.TriggerDragSkill, DragBanKuai);
	}
	
	void _clickBanKuai (RaycastHit arg1)
	{
		Collider col = arg1.collider;
		if (null != col && null != col.gameObject) {
			if (col.gameObject == gameObject || col.transform.IsChildOf (transform)) {
				data.AddHp (-1);
				print ("Bankuai:" + gameObject.name + " -- hp:" + data.hp);
//				foreach (BanKuaiState state in Enum.GetValues(typeof(BanKuaiState))) {
//					if (data.hp == (int)state) {
//						PlayAnim (state);
//					}
//				}
			}
		}
	}

	void DragBanKuai(SkillData data,RaycastHit hit)
	{
		Collider col = hit.collider;
		if (null != col && null != col.gameObject) {
			if (col.gameObject == gameObject || col.transform.IsChildOf (transform)) {
				if(data.type.ToString().Contains("Seal"))
				{
					Messenger.Broadcast (GameEventType.SealDragBanKuai, gameObject , (int)data.type % 10);
					data.SetCD ();
				}
				else if(data.type == SkillType.Skill2)
				{
					Messenger.Broadcast (GameEventType.SkillDragBanKuai, gameObject);
				}
			}
		}
	}

	void PlayAnim (BanKuaiState state)
	{
		string stateAnim = "bankuai" + gameObject.name + state.ToString ();
		if (simpanim.animatorHelper.HasState (stateAnim)) {
			simpanim.animatorHelper.Play (stateAnim, 1, true);
		}
	}

	public void PlayAnimator(int state)
	{
		mAnimator.SetInteger ("state", state);
	}

	public void SetSealEffect(GameObject obj)
	{
		mSealEffect = obj;
	}

	public void DestroySealEffect()
	{
		Destroy (mSealEffect);
	}
}
