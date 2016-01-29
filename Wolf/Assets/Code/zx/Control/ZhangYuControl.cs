using UnityEngine;
using System.Collections;

public class ZhangYuControl : SingleTonGO<ZhangYuControl>
{
	void Start ()
	{
		InitEvent ();
	}

	void OnDestroy ()
	{
		RemoveEvent ();
	}

	void InitEvent ()
	{
		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickZhangYu);
	}
	
	void RemoveEvent ()
	{
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickZhangYu);
	}

	void _clickZhangYu (RaycastHit arg1)
	{

	}
	 
	void OnCollisionEnter (Collision collision)
	{
		Collider col = collision.collider;
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				ZhangYuData.Me.hp -= enemy.attack;
				enemy.DestoryEnemy ();
			}
		}
	}
}
