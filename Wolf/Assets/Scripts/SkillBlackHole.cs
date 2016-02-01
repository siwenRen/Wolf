using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillBlackHole : MonoBehaviour {

	public float	totalTime = 5;

	// Use this for initialization
	void Start () {
	
		GetComponent<Collider> ().isTrigger = true;
		Invoke ("Destroy", totalTime);

		transform.position += (transform.position - PlanetControl.Me.transform.position).normalized * 0.5f;
		transform.up = (transform.position - PlanetControl.Me.gameObject.transform.position).normalized;

		ClipSound.Me.Play ("heidong_attack");

		ZhangYuControl.Me.PlayAnimator (ZhangYuControl.Me.skill3);
	}

	void OnTriggerEnter(Collider other) {
		Collider col = other.GetComponent<Collider>();
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				enemy.SetTarget(gameObject);
				enemy.SetStayTarget(gameObject);
				enemy.SetFaster(10);
			}
		}
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}
}
