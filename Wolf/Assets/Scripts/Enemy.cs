using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float movementSpeed; //2
	public int attack;

	private const float MaxX = 10;
	private const float MinX = -10;
	private const float MaxY = 10;
	private const float MinY = -10;

	private enum EMOVESTATE {
		None,
		MoveState,
		FlyState,
	};

	private GameObject	mPlanet;
	private GameObject	mParent;
	private Vector3		mTarget;
	private float 		mGravitySpeed = 500; //500
	private float		mFlySpeed = 3000;
	private EMOVESTATE	mMoveState;


	// Use this for initialization
	void Start ()
	{	
		Init ();
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener<RaycastHit> (GameEventType.CameraRayCastHit, Die);
	}

	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x > MaxX || transform.position.x < MinX || transform.position.y > MaxX || transform.position.y < MinY) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate ()
	{
		switch (mMoveState) {
		case EMOVESTATE.None:
			break;

		case EMOVESTATE.MoveState:
		{
//			transform.position += transform.forward * movementSpeed * Time.deltaTime;
			Vector3 dir = mTarget - transform.position;
			transform.position += dir.normalized * movementSpeed * Time.deltaTime;

			GetComponent<Rigidbody> ().AddForce (-transform.up * Time.deltaTime * mGravitySpeed); 
		}
			break;

		case EMOVESTATE.FlyState:
		{
			Vector3 dir = transform.position - PlanetControl.Me.gameObject.transform.position;
			GetComponent<Rigidbody> ().AddForce (dir.normalized * Time.deltaTime * mFlySpeed); 
		}
			break;
		}
	}

	private void Init ()
	{
		Vector3 dir = transform.position - PlanetControl.Me.gameObject.transform.position;
		transform.up = dir.normalized;

		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, Die);

		mMoveState = EMOVESTATE.MoveState;
	}

	void Die (RaycastHit hit)
	{
		if (hit.collider.gameObject == gameObject) {
			DestroyEnemy ();
		}
	}

	public void DestroyEnemy ()
	{
		Destroy (gameObject);
		ClipSound.Me.Play ("xiaoren_hit");
	}

	public void FlyAway()
	{
		mMoveState = EMOVESTATE.FlyState;
	}

	public void RandomPosition ()
	{
		float x = Random.Range (-1, 1);
		float y = Random.Range (-1, 1);
		float z = Random.Range (-1, 1);
//		Vector3 targetPos = mPlanet.transform.position + new Vector3 (x,y,z).normalized * 5f;
		Vector3 targetPos = PlanetControl.Me.gameObject.transform.position + new Vector3 (x, y, z).normalized * 3.5f;

		SetPosition (targetPos);
	}

	public void SetPosition (Vector3 pos)
	{
		transform.position = pos;
	}

	public void SetParent (GameObject obj)
	{
		mParent = obj;
	}

	public void SetTarget (Vector3 pos)
	{
		mTarget = pos;
	}
}
