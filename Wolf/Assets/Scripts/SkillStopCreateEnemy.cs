using UnityEngine;
using System.Collections;

public class SkillStopCreateEnemy : MonoBehaviour
{

	public float	duration = 5f;
	private float	time = 0;
	private EnemyFactory	factory;
	private GameObject		effect;

	// Use this for initialization
	void Start ()
	{
	
		factory = GetComponent<EnemyFactory> ();
		factory.StopCreate ();

		effect = Instantiate (Resources.Load ("Model/Stop", typeof(GameObject))) as GameObject;
		if (effect != null) {
			effect.transform.position = transform.position;
			effect.transform.up = (transform.position - PlanetControl.Me.gameObject.transform.position).normalized;
		}

		ClipSound.Me.Play ("ganrao_attack");

		ZhangYuControl.Me.PlayAnimator (ZhangYuControl.Me.skill2);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		time += Time.deltaTime;
		
		if (time > duration) {
			factory.StartCreate ();
			Destroy (effect);
		}
	}
}
