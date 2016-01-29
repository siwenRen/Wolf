using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	static CameraControl me;

	public static CameraControl Instance {
		get {
			return me;
		}
	}

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
		me = this;
		cc = Camera.main.GetComponent<CameraController> ();
		cc.SetDefault ();

		AddListenerEvent ();
		RemoveListenerEvent ();
	}

	void AddListenerEvent ()
	{
		Messenger.AddListener<float, float> (GameEventType.CameraShake, _shakeCamera);
	}

	void RemoveListenerEvent ()
	{
		Messenger.RemoveListener<float, float> (GameEventType.CameraShake, _shakeCamera);
	}

	void _shakeCamera (float strengths = 1.0f, float time = 1.0f)
	{
		LeanTween.cancel (gameObject);
		LeanTween.value (gameObject, (f) => {
			cc.positionOffset = Random.insideUnitSphere * strengths;
		}, 0, 1, time);
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

	void ClickTrigger (Vector3 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayhit;
		if (Physics.Raycast (ray, out rayhit, 100)) {
			Messenger.Broadcast (GameEventType.ClickSphere, rayhit);
		}
	}
}
