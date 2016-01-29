using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	public string 	prefabName;
	public float 	repeatMax;
	public float 	repeatMin;

	private string 	mPath = "Model/";
	private float	mTime;
	private float	mTotalTime;

	void Start(){

		mTime = 0.0f;
		mTotalTime = Random.Range (repeatMin, repeatMax);
	}

	void Update () {

		mTime += Time.deltaTime;
		if (mTime > mTotalTime) {
			mTime = 0.0f;
			mTotalTime = Random.Range (repeatMin, repeatMax);
			Create();
		}
	}

	public void Create()
	{
		GameObject go = Instantiate(Resources.Load( mPath + prefabName, typeof(GameObject))) as GameObject;
		Enemy enemy = go.GetComponent<Enemy> ();

		enemy.SetParent (this.gameObject);
		enemy.SetPosition(this.transform.position);
	}
}
