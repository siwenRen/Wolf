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
				ZhangYuData.Me.hp -= 1;

				GameObject.DestroyImmediate (col.gameObject);
			}
		}
	}
}
