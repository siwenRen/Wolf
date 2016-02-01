using UnityEngine;
using System.Collections;

public class ZhangYuData : SingleTonGO<ZhangYuData>
{
	public int hp;
	public int maxHp;
	public int mp;
	public int maxMp;

	public float hpPct {
		get {
			return Mathf.Clamp (hp * 1.0f / maxHp, 0, 1);
		}
	}

	public float mpPct {
		get {
			return Mathf.Clamp (mp * 1.0f / maxMp, 0, 1);
		}
	}

	public void AddHp (int changeValue)
	{
		if (hp >= 0) {
			hp += changeValue;
			if (hp <= 0) {
				GameProgress.Me.nowstate = GameProgress.GameState.Lose;
			}
		}
	}

	public void Reset ()
	{
		hp = maxHp;
	}

	void Start ()
	{
		Messenger.AddListener (GameEventType.GameStartEvent, Reset);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEventType.GameStartEvent, Reset);
	}
}

