using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	private CameraController cc;

	private bool touching = false;
	private Vector3 lastInput;

	private bool GetCurrentTouchPoint(out Vector3 point)
	{
		if (Input.GetMouseButton (0)) {
			point = Input.mousePosition;
			return true;
		}
		point = Vector3.zero;
		return false;
	}
	
	void Start ()
	{
		cc = Camera.main.GetComponent<CameraController> ();
		cc.SetDefault ();
	}

	void Update ()
	{
		if (touching) {
			Vector3 point;
			if (GetCurrentTouchPoint(out point) && !Vector3.Equals(lastInput, point))
			{
				if (null != cc) {
					var myAngel = cc.cameraRotationEuler;
					var input = point-lastInput;
					myAngel.x += input.y;
					myAngel.y += input.x;
					cc.RotateTo (myAngel);
				}
				lastInput = point;
			}
			else
			{
				touching = false;
			}
		} else {
			Vector3 point;
			if (GetCurrentTouchPoint(out point))
			{
				lastInput = point;
				touching = true;
			}
		}
	}
}
