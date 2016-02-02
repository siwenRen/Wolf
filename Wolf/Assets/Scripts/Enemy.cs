using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public EMOVESTATE	moveState;
	public float 	movementSpeed; //2
	public float	flySpeedMax;
	public float	flySpeedMin;
	public int 		attack;
	private const float MaxX = 6;
	private const float MinX = -6;
	private const float MaxY = 6;
	private const float MinY = -6;
	private const float	MaxZ = 6;
	private const float MinZ = -6;

	public enum EMOVESTATE
	{
		None,
		MoveState,
		FlyState,
		StayState,
	};

	private GameObject	mPlanet;
	private EnemyFactory	mParent;
	private GameObject	mTarget;
	private GameObject	mStayTarget;
	private float 		mGravitySpeed = 500; //500
	private float		mFlySpeed;
	private float		mOrginalMovementSpeed;//orginal speed
	private Rigidbody	mRigidbody;
	private Collider	mCollider;

	// Use this for initialization
	void Start ()
	{	
		Init ();
	}

	void OnDestroy ()
	{
//		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, Die);
		Messenger.RemoveListener (GameEventType.SkillSlowDown, SlowDown);
		Messenger.RemoveListener (GameEventType.GameStartEvent, DestroyEnemy);
	}

	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x > MaxX || transform.position.x < MinX || 
			transform.position.y > MaxY || transform.position.y < MinY ||
			transform.position.z > MaxZ || transform.position.z < MinZ) {
			DestroyEnemy ();
		}
	}

	void FixedUpdate ()
	{
		switch (moveState) {
		case EMOVESTATE.None:
			break;

		case EMOVESTATE.MoveState:
			{
				if (mTarget == null) {
					ResetSlowDown();
					mTarget = ZhangYuControl.Me.gameObject;
				}

//			transform.position += transform.forward * movementSpeed * Time.deltaTime;
				Vector3 dir = mTarget.transform.position - transform.position;
				transform.position += dir.normalized * movementSpeed * Time.deltaTime;
				//mRigidbody.velocity = dir.normalized;

				mRigidbody.AddForce (-transform.up * Time.deltaTime * mGravitySpeed); 

			}
			break;

		case EMOVESTATE.FlyState:
			{
				Vector3 dir = transform.position - PlanetControl.Me.gameObject.transform.position;
				mRigidbody.AddForce (dir.normalized * Time.deltaTime * mFlySpeed); 

				mFlySpeed -= 50;
				if (mFlySpeed <= 0) {
					moveState = EMOVESTATE.None;
				}
			}
			break;

		case EMOVESTATE.StayState:
			{
				if (mStayTarget == null) {
					SetWake ();
					moveState = EMOVESTATE.MoveState;
				}
			}
			break;
		}
	}

	private void Init ()
	{
		Vector3 dir = transform.position - PlanetControl.Me.gameObject.transform.position;
		transform.up = dir.normalized;

//		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, Die);
		Messenger.AddListener (GameEventType.SkillSlowDown, SlowDown);
		Messenger.AddListener (GameEventType.GameStartEvent, DestroyEnemy);

		moveState = EMOVESTATE.MoveState;
		mFlySpeed = Random.Range (flySpeedMin, flySpeedMax);
		mOrginalMovementSpeed = movementSpeed;

		mRigidbody = GetComponent<Rigidbody> ();
		mCollider = GetComponent<Collider> ();
	}

//	void Die (RaycastHit hit)
//	{
//		if (hit.collider.gameObject == gameObject) {
//			DestroyEnemy ();
//			SealData.Me.AddSealValue (1);
//
//			string[] path = { "xiaoren_dia_1" , "xiaoren_dia_2" };
//			int random = Random.Range (0, 1);
//			ClipSound.Me.Play (path [random]);
//		}
//	}

	public void DestroyEnemy ()
	{
		Destroy (gameObject);
		mParent.enemyList.Remove (this);
	}

	public void FlyAway ()
	{
		moveState = EMOVESTATE.FlyState;
//		ClipSound.Me.Play ("xiaoren_hit");
	}

	public void RandomPosition ()
	{
		float x = Random.Range (-1, 1);
		float y = Random.Range (-1, 1);
		float z = Random.Range (-1, 1);
//		Vector3 targetPos = mPlanet.transform.position + new Vector3 (x,y,z).normalized * 5f;
		Vector3 targetPos = PlanetControl.Me.gameObject.transform.position + new Vector3 (x, y, z).normalized * PlanetControl.Me.radius;

		SetPosition (targetPos);
	}

	//for black hole
	void OnCollisionEnter (Collision collision)
	{
		if (!mStayTarget) 
			return;

		Collider col = collision.collider;
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				SetSleep ();
				moveState = EMOVESTATE.StayState;
			}

		}
	}

	public void SetSleep ()
	{
		mRigidbody.Sleep ();
		mRigidbody.isKinematic = true;
	}

	public void SetWake ()
	{
		mRigidbody.WakeUp ();
		mRigidbody.isKinematic = false;
	}

	public void SlowDown ()
	{
		gameObject.AddComponent<SkillSlowDown> ();
	}

	public void SetSlowDown (float speed)
	{
		movementSpeed -= speed;
		if (movementSpeed < 0) {
			movementSpeed = 0;
		}
	}

	public void ResetSlowDown ()
	{
		movementSpeed = mOrginalMovementSpeed;
	}

	public void SetFaster(float ods)
	{
		movementSpeed = movementSpeed * ods;
	}

	public void SetPosition (Vector3 pos)
	{
		transform.position = pos;
	}

	public void SetParent (EnemyFactory factory)
	{
		mParent = factory;
	}

	public void SetTarget (GameObject obj)
	{
		mTarget = obj;
	}

	public void SetStayTarget (GameObject obj)
	{
		mStayTarget = obj;
	}
}
