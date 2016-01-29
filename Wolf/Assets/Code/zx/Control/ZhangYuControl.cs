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
				int damage = col.gameObject.GetComponent<Enemy> ().attack;
				ZhangYuData.Me.hp -= damage;
				ZhangYuData.Me.hp = Mathf.Max (0, ZhangYuData.Me.hp);
				print ("zhangyu jianxue -- " + ZhangYuData.Me.hp);
				GameObject.Destroy (col.gameObject);
			}
		}
	}
}
