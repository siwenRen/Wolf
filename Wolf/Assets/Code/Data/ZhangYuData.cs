using UnityEngine;
using System.Collections;

public class ZhangYuData : SingleTonGO<ZhangYuData>
{
	public int hp = 150;
	public int maxHp = 150;
	public int mp = 150;
	public int maxMp = 150;

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

