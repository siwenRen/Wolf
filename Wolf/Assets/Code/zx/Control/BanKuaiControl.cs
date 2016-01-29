using UnityEngine;
using System.Collections;

public class BanKuaiControl : MonoBehaviour
{
	public BanKuaiData data;

	void Start ()
	{
		if (null == data) {
			data = Utils.Instance.GetOrAddComponent<BanKuaiData> (gameObject);
		}

		InitEvent ();
	}
	
	void OnDestroy ()
	{
		RemoveEvent ();
	}
	
	void InitEvent ()
	{
		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickBanKuai);
	}
	
	void RemoveEvent ()
	{
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, _clickBanKuai);
	}
	
	void _clickBanKuai (RaycastHit arg1)
	{
		Collider col = arg1.collider;
		if (null != col && null != col.gameObject) {
			if (col.gameObject == gameObject) {
				ClipSound.Me.Play ();
				data.hp -= 1;
				print ("BanKuai --HP " + gameObject.name + " " + data.hp);
			}
		}
	}
}
