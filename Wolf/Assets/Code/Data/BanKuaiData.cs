using UnityEngine;
using System.Collections;

public class BanKuaiData : MonoBehaviour
{
	public int hp = 15;

	public void AddHp (int changeValue)
	{
		if (hp > 0) {
			hp += changeValue;
		}
	}
	
	void Start ()
	{
		Messenger.AddListener (GameEventType.GameStartEvent, Reset);
	}
	
	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEventType.GameStartEvent, Reset);
	}

	void Reset ()
	{
		hp = 15;
	}
}
