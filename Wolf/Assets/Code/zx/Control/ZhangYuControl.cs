using UnityEngine;
using System.Collections;

public class ZhangYuControl : SingleTonGO<ZhangYuControl>
{
	public string live = "live";

	public string skill1 = "haliluya1";
	public string skill2 = "haliluya2";
	public string skill3 = "haliluya3";
	public string skill4 = "haliluya4";
	public string turnLeft = "haliluyaL";
	public string turnRight = "haliluyaR";
	public string start = "haliluya";
	public string end = "haliluyast";

	private Animator	mAnimator;

	void Start ()
	{
		InitEvent ();

		mAnimator = GetComponent<Animator> ();

		SetChildActive (false);
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
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				ZhangYuData.Me.AddHp (-enemy.attack);
//				ZhangYuData.Me.AddHp (-100);
				enemy.DestroyEnemy ();
			}
		}
	}

	void LateUpdate ()
	{
//		Vector3 forward = (CameraControl.Me.transform.position - PlanetControl.Me.transform.position).normalized;
//		if (!Vector3.Equals (transform.forward, forward)) {
//			transform.forward = forward;
//			transform.forward = Vector3.Lerp (transform.forward, forward, RealTime.deltaTime);
//		}
	}

	public void SetAnimator(string name , int state)
	{
		mAnimator.SetInteger ( name , state);
	}

	public void PlayAnimator(string name)
	{
		mAnimator.Play (name);
	}

	public void StartAnimator()
	{
		PlayAnimator (start);
	}

	public void EndAnimator()
	{
		PlayAnimator (end);
	}

	public void TurnLeft()
	{
		PlayAnimator (turnLeft);
	}

	public void TurnRight()
	{
		PlayAnimator (turnRight);
	}

	public void Die()
	{
		SetAnimator (live, 1);
	}

	public void Live()
	{
		SetAnimator (live, 0);
	}

	public void SetChildActive(bool active)
	{
		for (int i = 0; i < transform.childCount; ++i) {
			transform.GetChild(i).gameObject.SetActive(active);
		}
	}

	public void Show()
	{
		SetChildActive (true);
		BgSound.Me.PlayBg1 ();
	}

	public void Hide()
	{
		SetChildActive (false);
		BgSound.Me.PlayBg0 ();
	}
}
