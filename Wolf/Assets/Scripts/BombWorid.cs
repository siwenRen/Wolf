using UnityEngine;
using System.Collections;

public class BombWorid : MonoBehaviour {

	public float	duration = 10f;
	public float 	repeatTime = 0.1f;

	private float	time = 0.0f;
	private float	totalTime = 0.0f;

	// Use this for initialization
	void Start () {
	
		CameraControl.Me.ShakeCamera (0.05f, duration);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (totalTime > duration) {
			Component.Destroy(this);
		}

		time += Time.deltaTime;
		if (time > repeatTime) {
			CreateBomb();
			time = 0.0f;
			totalTime += repeatTime;
		}
	}
	
	public void CreateBomb()
	{
		GameObject bomb = Instantiate(Resources.Load( "Model/Explosion", typeof(GameObject))) as GameObject;
		if (bomb != null) {
			
			float x = Random.Range (-1.0f, 1.0f);
			float y = Random.Range (-1.0f, 1.0f);
			float z = Random.Range (-1.0f, 1.0f);
			Vector3 targetPos = PlanetControl.Me.gameObject.transform.position + new Vector3 (x, y, z).normalized * PlanetControl.Me.radius;
			
			bomb.transform.position = targetPos;
			bomb.transform.up = (targetPos - PlanetControl.Me.gameObject.transform.position).normalized;
		}
	}
}
