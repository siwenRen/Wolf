using UnityEngine;
using System.Collections;

public class NormalAttackControl : MonoBehaviour {

	private Collider	mCollider;

	void Start(){
		mCollider = gameObject.GetComponent<Collider> ();
	}

	void OnCollisionEnter (Collision collision)
	{
		Collider col = collision.collider;
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				enemy.FlyAway ();
			}
		}
	}

	public void DisableCollision()
	{
		if (mCollider != null) {
			mCollider.enabled = false;
		}
	}
}
