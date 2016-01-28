using UnityEngine;
using System.Collections;

public class MessagerTest : MonoBehaviour
{
	public const string TestEvent = "-----";
	// Use this for initialization
	void Start ()
	{
		Messenger.AddListener (TestEvent, _testEvent);
		Messenger.Broadcast (TestEvent);
	}

	void _testEvent ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
