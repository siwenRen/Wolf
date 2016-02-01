using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
	public Transform target;
	public bool pos;
	public bool rotation;
	
	void LateUpdate ()
	{
		if (pos && !Vector3.Equals (transform.position, target.position)) {
			transform.position = target.position;
		}
		if (rotation && !Quaternion.Equals (transform.rotation, target.rotation)) {
			transform.rotation = target.rotation;
		}
	}
}
