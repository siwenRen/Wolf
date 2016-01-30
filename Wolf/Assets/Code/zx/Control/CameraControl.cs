using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraController))]
public class CameraControl : SingleTonGO<CameraControl>
{
	public float judgeDistance = 100;
	public bool lock_y = false;
	public bool lock_x = false;
	public bool isTriggerClick = true;
	public bool clickui;
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

	public void ShakeCamera (float strengths = 1.0f, float time = 1.0f)
	{
		LeanTween.cancel (gameObject);
		LeanTween.value (gameObject, (f) => {
			cc.positionOffset = Random.insideUnitSphere * strengths;
		}, 0, 1, time);
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			clickui = UICamera.isOverUI;
		} else if (Input.GetMouseButtonUp (0)) {
			clickui = false;
		}
		if (clickui) {
			return;
		}

		if (!isTriggerClick) {
			return;
		}

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
				CameraRayCastHit (point);
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
					if (!lock_x) {
						myAngel.y += input.x;
					}
					cc.RotateTo (myAngel);
				}
				lastInput = point;
			} else {
				phase = Phase.None;
			}
			break;
		}
	}

	void CameraRayCastHit (Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayhit;
		if (Physics.Raycast (ray, out rayhit, 100)) {
			Messenger.Broadcast (GameEventType.CameraRayCastHit, rayhit);
		}
	}
}
