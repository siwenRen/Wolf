using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SealData : SingleTonGO<SealData>
{
	public List<int> sealBreakPoint;
	public float nowSealValue;
	public int nowBreakState;
	public bool islock = false;

	void Start ()
	{
		InitState ();
	}

	void InitState ()
	{
		nowBreakState = 0;
	}

	public void AddSealValue (int addValue)
	{
		if (!islock) {
			nowSealValue += addValue;
			int breakPoint = sealBreakPoint [nowBreakState];
			if (nowSealValue >= breakPoint) {
				islock = true;
				Messenger.Broadcast (GameEventType.SealBreak, nowBreakState);
				nowBreakState += 1;
			}
		}
	}

	public void UseSeal ()
	{
		int breakPoint = sealBreakPoint [nowBreakState];
		if (islock && nowSealValue >= breakPoint) {
			
		}
	}
}
