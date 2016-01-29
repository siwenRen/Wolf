using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	public GameObject effectTarget;
	public float judgeDistance = 100;
	public bool lock_y = false;
	private CameraController cc;

	enum Phase
	{
		None,
		Judge,
		Camera
	}

	private Phase phase = Phase.None;
	private Vector3 lastInput;

	private bool GetCurrentTouchPoint (out Vector3 point)
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
		Vector3 point;
		switch (phase) {
		case Phase.None:
			if (GetCurrentTouchPoint (out point)) {
				lastInput = point;
				phase = Phase.Judge;
			}
			break;
		case Phase.Judge:
			if (GetCurrentTouchPoint (out point)) {
				if (Vector3.Distance (lastInput, point) > judgeDistance) {
					lastInput = point; // smooth
					phase = Phase.Camera;
				}
			} else {
				// attack
//				ShakeCamera ();
				ClickTrigger (point);
				phase = Phase.None;
			}
			break;
		case Phase.Camera:
			if (GetCurrentTouchPoint (out point)) {
				if (null != cc) {
					var myAngel = cc.cameraRotationEuler;
					var input = point - lastInput;
					if (!lock_y) {
						myAngel.x += input.y;
					}
					myAngel.y += input.x;
					cc.RotateTo (myAngel);
				}
				lastInput = point;
			} else {
				phase = Phase.None;
			}
			break;
		}
	}

	void ShakeCamera ()
	{
		cc.positionOffset = Random.insideUnitSphere;
	}

	void ClickTrigger (Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayhit;
		if (Physics.Raycast (ray, out rayhit, 100)) {
			if (effectTarget) {
				effectTarget.transform.position = rayhit.point;
				effectTarget.transform.up = rayhit.normal;
			}
		}
	}
}
