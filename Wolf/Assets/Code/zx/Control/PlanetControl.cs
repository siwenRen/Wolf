using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetControl : SingleTonGO<PlanetControl>
{
	public float radius = 3.6f;
	public List<BanKuaiControl>	bankuaiList = new List<BanKuaiControl>();
	public List<EnemyFactory> factoryList = new List<EnemyFactory>();
	public int factoryCount;

	void Start ()
	{
		factoryCount = 7;
	}

	public void PlayBackPlanet()
	{
		for(int i = 0 ; i < bankuaiList.Count ; ++i)
		{
			bankuaiList[i].PlayAnimator(2);
			bankuaiList[i].DestroySealEffect();
		}

		for (int i = 0; i < factoryList.Count; ++i) {
			factoryList[i].RemoveAllEnemy();
			factoryList[i].gameObject.SetActive(true);
		}

		factoryCount = 7;
	}
}
