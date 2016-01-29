using UnityEngine;
using System.Collections;

public class EnemyFactory : MonoBehaviour {

	void Start(){
		Create("Model/Enemy");
	}

	public void Create(string path)
	{
		GameObject go = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
		Enemy enemy = go.GetComponent<Enemy> ();

		enemy.SetParent (this.gameObject);
		enemy.RandomPosition ();
	}
}
