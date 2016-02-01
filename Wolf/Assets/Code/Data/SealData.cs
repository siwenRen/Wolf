using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SealData : SingleTonGO<SealData>
{
	public List<int> sealBreakPoint;
	public float nowSealValue;
	public int nowBreakState;
	public bool islock = false;

	public float sealPct {
		get {
			if (nowBreakState < sealBreakPoint.Count) {
				int breakPoint = sealBreakPoint [nowBreakState];
				return nowSealValue * 1.0f / breakPoint;
			}
			return 0;
		}
	}

	void Start ()
	{
		InitState ();
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEventType.GameStartEvent, reset);
		Messenger.RemoveListener (GameEventType.GameFail, reset);
	}

	void InitState ()
	{
		nowBreakState = 0;
		islock = false;

		Messenger.AddListener (GameEventType.GameStartEvent, reset);
		Messenger.AddListener (GameEventType.GameFail, reset);
	}
	
	void reset ()
	{
		nowSealValue = 0;
		nowBreakState = 0;
		islock = false;
	}

	public void AddSealValue (int addValue)
	{
		if (!islock && GameProgress.Me.nowstate == GameProgress.GameState.Playing) {
			nowSealValue += addValue;
			if (nowBreakState < sealBreakPoint.Count) {
				int breakPoint = sealBreakPoint [nowBreakState];
				if (nowSealValue > breakPoint) {
					islock = true;
					Messenger.Broadcast (GameEventType.SealBreak, GetSealSkillType ());
				}
			}
		}
	}

	public void UseSeal ()
	{
		if (islock && sealPct >= 1) {
			islock = false;
			nowSealValue = 0;
			nowBreakState += 1;

			Messenger.Broadcast (GameEventType.UseSeal, nowBreakState);
		}
	}

	public SkillType GetSealSkillType ()
	{
		if (sealPct >= 1) {
			return (SkillType)(nowBreakState + 11);
		}
		return SkillType.Attack;
	}
}
