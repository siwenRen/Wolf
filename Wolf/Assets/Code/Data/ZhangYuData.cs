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
}

