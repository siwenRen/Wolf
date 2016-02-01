using UnityEngine;
using System.Collections;

public class SkillBomb : MonoBehaviour
{

	public float	duration = 1f;
	private float	time = 0;

	void Start ()
	{
		ClipSound.Me.Play ("quanqiubaozha");
	}

	void Update ()
	{
		time += Time.deltaTime;

		if (time > duration) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		Collider col = collision.gameObject.GetComponent<Collider> ();
		if (null != col && null != col.gameObject) {
			if (LayerMask.LayerToName (col.gameObject.layer) == "WuQi") {
				Enemy enemy = col.gameObject.GetComponent<Enemy> ();
				SealData.Me.AddSealValue (1);
				enemy.DestroyEnemy ();
			}
		}
	}
}
