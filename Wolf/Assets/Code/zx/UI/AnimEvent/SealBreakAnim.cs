using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SimpleAnimatorPlayer))]
public class SealBreakAnim : MonoBehaviour
{
	public SimpleAnimatorPlayer simp;
	public UILabel name;
	
	public void SealBreakEnd ()
	{
//		Messenger.Broadcast (GameEventType.GameStartEvent);
		print ("SealBreakEnd");
	}

	public void PlaySealBreak (SkillType type)
	{
		gameObject.SetActive (true);

		SkillData sealData = SkillControl.Me.skillMap [type];
		name.text = sealData.skillName;
		simp.animatorHelper.Play ("SealBreak", 1, true);
	}

	public void Reset ()
	{

	}

	public void Hide ()
	{
		GetComponent<UIPanel> ().alpha = 0;
	}

	public void Show ()
	{
		GetComponent<UIPanel> ().alpha = 1;
	}
}
