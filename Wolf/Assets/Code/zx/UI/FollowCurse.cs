using UnityEngine;
using System.Collections;

public class FollowCurse : MonoBehaviour
{
	public bool open = false;
	Vector3 away = new Vector3 (10000, 0, 0);
	Camera uicamera;

	void Start ()
	{
		uicamera = NGUITools.FindCameraForLayer (LayerMask.NameToLayer ("UI"));
	}

	// Update is called once per frame
	void Update ()
	{
		if (open) {
			transform.position = uicamera.ScreenToWorldPoint (Input.mousePosition);
		} else {
			if (!Vector3.Equals (transform.localPosition, away)) {
				transform.position = away;
			}
		}
	}
}
