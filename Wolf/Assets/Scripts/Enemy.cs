using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float 	movementSpeed; //2
	public int	attack;

	private float 	mGravitySpeed = 500; //500
	private GameObject	mParent;
	private Vector3		mTarget;
	private bool move;

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
	
	}

	void FixedUpdate ()
	{

		if (move) {
//			transform.position += transform.forward * movementSpeed * Time.deltaTime;

			Vector3 dir = mTarget - transform.position;
			transform.position += dir.normalized * movementSpeed * Time.deltaTime;
		}
		GetComponent<Rigidbody> ().AddForce (-transform.up * Time.deltaTime * mGravitySpeed); 
	}

	private void Init ()
	{
		Vector3 dir = transform.position - PlanetControl.Me.gameObject.transform.position;
//		Vector3 dd =  ZhangYuControl.Me.gameObject.transform.position-transform.position ;
//		transform.forward = dd.normalized;
		transform.up = dir.normalized;

		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit, Die);

		move = true;
	}

	public void Die (RaycastHit hit)
	{
		if (hit.collider.gameObject == gameObject) {
			DestroyEnemy();
		}
	}

	public void DestroyEnemy()
	{
		Destroy (gameObject);
		ClipSound.Me.Play ("xiaoren_hit");
	}

	public void RandomPosition ()
	{
		float x = Random.Range (-1, 1);
		float y = Random.Range (-1, 1);
		float z = Random.Range (-1, 1);
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

	public void SetTarget(Vector3 pos)
	{
		mTarget = pos;
	}
}
