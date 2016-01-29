using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	public string	mPrefabName;
	private float	mDelayTime;
	public float	mRepeatTimeMax;
	public float	mRepeatTimeMin;

	private float	mTime;
	private float	mTotalTime;

	private string	mPath = "Model/";

	void Start(){

		mTime = 0.0f;

		mTotalTime = Random.Range (mRepeatTimeMin, mRepeatTimeMax);
//		InvokeRepeating( "Create" , mDelayTime , mTotalTime );
	}

	void Update () {

		mTime += Time.deltaTime;
		if (mTime > mTotalTime) {
			mTime = 0.0f;
			mTotalTime = Random.Range (mRepeatTimeMin, mRepeatTimeMax);
			Create();
		}
	}

	public void Create()
	{
		GameObject go = Instantiate(Resources.Load(mPath + mPrefabName, typeof(GameObject))) as GameObject;
		Enemy enemy = go.GetComponent<Enemy> ();

		enemy.SetParent (gameObject);
		enemy.SetPosition (transform.position);
		enemy.SetTarget (ZhangYuControl.Me.gameObject.transform.position);
	}
}
