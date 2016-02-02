using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour
{

	public string 	prefabName = "xiaobing";
	public float 	repeatMax = 2f;
	public float 	repeatMin = 0.5f;
	public List<Enemy>	enemyList = new List<Enemy> ();
	public GameObject	effectPoint;
	private int		enemyCountMax = 20;

	private string 	mPath = "Model/";
	private float	mTime;
	private float	mTotalTime;
	private bool	mIsCreate;

	void Start ()
	{

		prefabName = "xiaobing";

		Init ();

		Messenger.AddListener (GameEventType.GamePlay, StartCreate);

		Messenger.AddListener (GameEventType.GameFail, StopCreate);
		Messenger.AddListener (GameEventType.GameWin, StopCreate);
		
		Messenger.AddListener<GameObject> (GameEventType.SkillDragBanKuai, StopCreateEnemy);
		Messenger.AddListener<GameObject , int> (GameEventType.SealDragBanKuai, DestroyEnemyFactory);
		Messenger.AddListener (GameEventType.RepairSeal, GetRandom);
	}

	public void Init()
	{
		mTime = 0.0f;
		mIsCreate = false;
		repeatMax = 2f;
		repeatMin = 0.5f;
		enemyCountMax = 20;
		mTotalTime = Random.Range (repeatMin, repeatMax);
	}

	void Update ()
	{

		if (enemyList.Count > enemyCountMax)
			return;

		if (GameProgress.Me.nowstate != GameProgress.GameState.Playing)
			return;

		if (mIsCreate && gameObject.activeSelf) {
			mTime += Time.deltaTime;
			if (mTime > mTotalTime) {
				mTime = 0.0f;
				mTotalTime = Random.Range (repeatMin, repeatMax);
				CreateTenEnemy ();
			}
		}
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (GameEventType.GamePlay, StartCreate);

		Messenger.RemoveListener (GameEventType.GameFail, StopCreate);
		Messenger.RemoveListener (GameEventType.GameWin, StopCreate);
		
		Messenger.RemoveListener<GameObject> (GameEventType.SkillDragBanKuai, StopCreateEnemy);
		Messenger.RemoveListener<GameObject , int> (GameEventType.SealDragBanKuai, DestroyEnemyFactory);
		Messenger.RemoveListener (GameEventType.RepairSeal, GetRandom);
	}

	public void CreateTenEnemy ()
	{
		for (int i = 0; i < 5; ++i) {
			Create ();
		}
	}

	public void Create ()
	{
		GameObject go = Instantiate (Resources.Load (mPath + prefabName, typeof(GameObject))) as GameObject;
		Enemy enemy = go.GetComponent<Enemy> ();

		float radius = 0.5f;
		Vector3 pos = Utils.Instance.RandomCreateSpherePoint (PlanetControl.Me.transform.position, 
		                                                      transform.position + (transform.position - PlanetControl.Me.transform.position).normalized * 0.2f,
		                                                      radius);

		enemy.SetParent (this);
		enemy.SetPosition (pos);
		enemy.SetTarget (ZhangYuControl.Me.gameObject);

		enemyList.Add (enemy);
	}

	public void StartCreate ()
	{
		mIsCreate = true;
	}

	public void StopCreate ()
	{
		mIsCreate = false;
	}

	public void StopCreateEnemy (GameObject obj)
	{
		if (transform.IsChildOf (obj.transform)) {
			gameObject.AddComponent<SkillStopCreateEnemy> ();
		}
	}

	public void DestroyEnemyFactory (GameObject obj, int nowSealId)
	{
		if (transform.IsChildOf (obj.transform) && gameObject.activeSelf) {
			print ("DestroyEnemyFactory --- ");
			Messenger.Broadcast (GameEventType.RepairSeal);

			Seal seal = gameObject.AddComponent<Seal> ();
			BanKuaiControl con = obj.GetComponent<BanKuaiControl> ();
			seal.SetParent (con);
			seal.SetId (nowSealId);

			SealData.Me.UseSeal ();
		}
	}

	public void RemoveAllEnemy ()
	{
		for (int i = 0; i < enemyList.Count; ++i) {
			Destroy (enemyList [i].gameObject);
		}

		enemyList.Clear ();
	}

	public void RemoveEnemy (Enemy enemy)
	{
		enemy.FlyAway ();
		enemyList.Remove (enemy);
	}

	public void GetRandom()
	{
		repeatMax = 2 * PlanetControl.Me.factoryCount / (7f * 1.5f);
		repeatMin = 0.5f * PlanetControl.Me.factoryCount / (7f * 1.5f);
		mTotalTime = Random.Range (repeatMin, repeatMax);
		enemyCountMax = 20 * 7 / PlanetControl.Me.factoryCount;
	}

	public void SetActiveChild(bool isActive)
	{
		for (int i = 0; i < transform.childCount; ++i) {
			transform.GetChild(i).gameObject.SetActive(isActive);
		}
	}
}
