using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject effectTarget;

	void Start ()
	{
		InitEvent ();
	}

	void OnDestroy ()
	{
		RemoveListenerEvent ();
	}

	void InitEvent ()
	{
		Messenger.AddListener<RaycastHit> (GameEventType.ClickSphere, _clickShpere);
	}

	void RemoveListenerEvent ()
	{
		Messenger.RemoveListener<RaycastHit> (GameEventType.ClickSphere, _clickShpere);
	}

	void _clickShpere (RaycastHit hit)
	{
		if (null != effectTarget) {
			effectTarget.transform.position = hit.point;
			effectTarget.transform.up = hit.normal;
		}
	}

	void CheckAnim ()
	{
		SimpleAnimatorPlayer simp = Utils.Instance.GetOrAddComponent<SimpleAnimatorPlayer> (effectTarget);
	}
}
