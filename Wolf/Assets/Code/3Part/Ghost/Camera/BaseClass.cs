using UnityEngine;

public class BaseClass
{
	public static void print (object msg)
	{
#if UNITY_EDITOR
			Debug.Log(msg + "\n" + StackTraceUtility.ExtractStackTrace ());
#endif
	}
	
	public static void log (object msg)
	{
//		UnityEngine.Debug.Log (msg);
	}
	
	public static void logOnScene (string msg)
	{
//		NGUIDebug.Log (msg);
	}
}
