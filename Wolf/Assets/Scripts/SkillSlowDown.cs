using UnityEngine;
using System.Collections;

public class SkillSlowDown : MonoBehaviour {

	public float	duration = 3.0f;
	public float	speed = 0.8f;

	private float	time;
	private Enemy 	enemy;

	// Use this for initialization
	void Start () {
	
		time = 0;

		enemy = gameObject.GetComponent<Enemy> ();
		enemy.SetSlowDown (speed);

		ClipSound.Me.Play ("jiansu_attack");

		ZhangYuControl.Me.PlayAnimator (ZhangYuControl.Me.skill4);
	}
	
	// Update is called once per frame
	void Update () {
	
		time += Time.deltaTime;

		if (time > duration) {
			enemy.ResetSlowDown();
			Component.Destroy(this);
		}
	}
}
