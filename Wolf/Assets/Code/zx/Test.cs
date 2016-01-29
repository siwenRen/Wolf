using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour
{
	public Transform child;
	// Use this for initialization
	void Start ()
	{
		print (child.IsChildOf (transform));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
