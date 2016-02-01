using UnityEngine;
using System.Collections;

public class NormalAttackControl : MonoBehaviour {

	private Collider	mCollider;

	void Start(){
		mCollider = gameObject.GetComponent<Collider> ();

		ZhangYuControl.Me.PlayAnimator (ZhangYuControl.Me.skill1);
	}

	void OnCollisionEnter (Collision collision)
	{
		FlyAwayEnemy (collision);
	}

	void OnCollisionStay (Collision collision)
	{
		FlyAwayEnemy (collision);
	}

	private void FlyAwayEnemy(Collision collision)
	{
		Collider col = collision.collider;
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				enemy.FlyAway ();
				SealData.Me.AddSealValue (1);
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
