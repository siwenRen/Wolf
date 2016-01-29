using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private GameObject	mPlanet;
	private GameObject	mParent;
	private GravityMove mGravityMove;

	// Use this for initialization
	void Start () {
	
		GetObject ();
		GetComponent ();
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GetObject()
	{
		if (mPlanet == null) {
			mPlanet = GameObject.Find ("SPlanet");
		}

	}

	private void GetComponent()
	{
		mGravityMove = this.GetComponent<GravityMove>();
		if (mGravityMove == null) {
			mGravityMove = gameObject.AddComponent<GravityMove> ();
		}
		
	}

	private void Init()
	{
		Vector3 dir = transform.position - mPlanet.transform.position;
		transform.up = dir.normalized;

		Messenger.AddListener<RaycastHit> (GameEventType.CameraRayCastHit , Die);

		mGravityMove.StartMove ();
	}

	public void Die(RaycastHit hit)
	{
		if (hit.collider.gameObject == gameObject) {
			Destroy (gameObject);
		}
	}

	public void RandomPosition()
	{
		float x = Random.Range (-1,1);
		float y = Random.Range (-1,1);
		float z = Random.Range (-1,1);
//		Vector3 targetPos = mPlanet.transform.position + new Vector3 (x,y,z).normalized * 5f;
		Vector3 targetPos = new Vector3 (x,y,z).normalized * 5.5f;

		SetPosition (targetPos);
	}

	public void SetPosition(Vector3 pos)
	{
		transform.position = pos;
	}

	public void SetParent(GameObject obj)
	{
		mParent = obj;
	}
}
