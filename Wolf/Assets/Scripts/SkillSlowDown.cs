using UnityEngine;
using System.Collections;

public class SkillSlowDown : MonoBehaviour {

	public float	duration = 6.0f;
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

		CameraControl.Me.ShakeCamera (0.05f, 1.5f);
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
