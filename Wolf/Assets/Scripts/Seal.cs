using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Seal : MonoBehaviour {

	public float	duration = 5f;
	public int		id;

	private float	time = 0;
	private float	timeDie = 0;
	private EnemyFactory	factory;
	private BanKuaiControl	parentControl;
	private GameObject		effect;
	private List<GameObject>	fragObject = new List<GameObject>();


	// Use this for initialization
	void Start () {

		factory = GetComponent<EnemyFactory> ();
		factory.StopCreate ();


		effect = Instantiate(Resources.Load( "Model/Ca" + id.ToString(), typeof(GameObject))) as GameObject;
		if (effect != null) {
			//effect.transform.position = transform.position;
			effect.transform.parent = factory.effectPoint.transform;
			effect.transform.localPosition = Vector3.zero;
			effect.transform.localRotation = Quaternion.Euler(Vector3.zero);
			effect.SetActive(false);
			parentControl.SetSealEffect(effect);
		}

		for(int i = 0 ; i < parentControl.transform.GetChild(0).childCount ; ++i)
		{
			GameObject obj = parentControl.transform.GetChild(0).GetChild(i).gameObject;
			if(obj.name.Contains("frag"))
			{
				fragObject.Add(obj);
			}
		}

		parentControl.PlayAnimator (1);
		
		CameraControl.Me.ShakeCamera (0.05f, duration);
		
		InvokeRepeating ("RandomBombOnChild", 0f , 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
			if(factory.enemyList.Count > 0)
			{
				timeDie += Time.deltaTime;
				if(timeDie >= 0.1f)
				{
					timeDie = 0.0f;
					factory.RemoveEnemy(factory.enemyList[0]);
				}

//				state = ESTATE.PlanetBreak;
//				return;
			}

			time += Time.deltaTime;
			
			if (time > duration) {
				
				if(id == 7)
				{
					GameProgress.Me.nowstate = GameProgress.GameState.Win;
				}
				
				--PlanetControl.Me.factoryCount;
			Debug.Log("~~~~~~~~~~~~~" + PlanetControl.Me.factoryCount);
				gameObject.SetActive(false);
				effect.SetActive(true);
				Component.Destroy(this);
			}
	}

	public void SetParent(BanKuaiControl con)
	{
		parentControl = con;
	}

	public void SetId(int sealId)
	{
		id = sealId;
	}

	public void RandomBombOnChild()
	{
		int num = Random.Range (0, (fragObject.Count - 1));

		GameObject bomb = Instantiate(Resources.Load( "Model/Explosion" + id.ToString(), typeof(GameObject))) as GameObject;
		if (bomb != null) {
			bomb.transform.position = fragObject[num].transform.position;
			bomb.transform.up = (fragObject[num].transform.position - PlanetControl.Me.gameObject.transform.position).normalized;
		}

		ClipSound.Me.Play ("fengyinbaozha" + id.ToString());
	}
}
