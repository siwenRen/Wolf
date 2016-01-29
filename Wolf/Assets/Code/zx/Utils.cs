using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils
{
	static Utils instance;

	public static Utils Instance {
		get {
			if (null == instance) {
				instance = new Utils ();
			}
			return instance;
		}
	}
	
	public void SetDepths (GameObject root, int depth, int index = 0)
	{
		var widget = root.transform.GetComponent<UIWidget> ();
		if (widget != null)
			widget.depth = depth + index;
		
		index += 1;
		foreach (Transform c in root.transform) {
			widget = c.GetComponent<UIWidget> ();
			if (widget != null)
				widget.depth = depth;
			this.SetDepths (c.gameObject, depth, index);
		}
	}
	
	public GameObject DeepFind (GameObject parent, string name)
	{
		foreach (Transform c in parent.transform) {
			if (c.name == name)
				return c.gameObject;
			else {
				GameObject resultGo = DeepFind (c.gameObject, name);
				if (resultGo != null)
					return resultGo;
			}
		}
		return null;
	}
	
	public GameObject DeepFindChild (GameObject parent, string name)
	{
		foreach (Transform c in parent.transform) {
			if (c.name == name || c.name.StartsWith (name))
				return c.gameObject;
			else {
				GameObject resultGo = DeepFindChild (c.gameObject, name);
				if (resultGo != null)
					return resultGo;
			}
		}
		return null;
	}
	
	public GameObject[] DeepFindChildren (GameObject parent, string name)
	{
		List<GameObject> objs = new List<GameObject> ();
		if (parent.transform.childCount > 0) {
			foreach (Transform c in parent.transform) {
				if (c.name == name) {
					objs.Add (c.gameObject);
				}
				objs.AddRange (DeepFindChildren (c.gameObject, name));
			}
		} 
		return objs.ToArray ();
	}
	
	public GameObject[] GetAllChildren (GameObject parent)
	{
		if (parent == null) {
			return null;
		}
		List<GameObject> results = new List<GameObject> ();
		foreach (Transform t in parent.transform) {
			if (t != null) {
				results.Add (t.gameObject);
				
				GameObject[] cs = GetAllChildren (t.gameObject);
				if (cs != null) {
					results.AddRange (cs);
				}
			}
		}
		return results.ToArray ();
	}
	
	public bool DestroyAllChildren (GameObject parent)
	{
		if (parent == null)
			return false;
		
		int count = parent.transform.childCount;
		for (int i = 0; i<count; i++) {
			Transform t = parent.transform.GetChild (0);
			if (t != null)
				GameObject.DestroyImmediate (t.gameObject);
		}
		return true;
	}
	
	/// <summary>
	/// get All Comps with noActive 
	/// </summary>
	public Component[] GetAllComponentsInChildren (GameObject p, System.Type t, bool containSelf = true)
	{
		List<Component> comps = new List<Component> ();
		Component sp = p.GetComponent (t);
		if (containSelf && sp != null)
			comps.Add (sp);
		
		foreach (Transform child in p.transform) {
			Component ct = child.GetComponent (t);
			if (ct != null) {
				comps.Add (ct);
			}
			comps.AddRange (GetAllComponentsInChildren (child.gameObject, t, false));
		}
		return comps.ToArray ();
	}
	
	public void ChangeLayersRecursively (GameObject parent, string LayerName)
	{
		if (parent.layer != LayerMask.NameToLayer (LayerName)) {
			parent.layer = LayerMask.NameToLayer (LayerName);
		}
		foreach (Transform c in parent.transform) {
			ChangeLayersRecursively (c.gameObject, LayerName);
		}
	}
	
	public void ChangeLayersRecursively (GameObject parent, int layer)
	{
		if (parent.layer != layer) {
			parent.layer = layer;
		}
		foreach (Transform c in parent.transform) {
			ChangeLayersRecursively (c.gameObject, layer);
		}
	}
	
	public GameObject CopyObjTo (GameObject obj, Transform parent)
	{
		if (parent == null) {
			parent = obj.transform.parent;
		}
		GameObject copy = GameObject.Instantiate (obj) as GameObject;
		if (!copy.activeInHierarchy) {
			copy.SetActive (true);
		}
		copy.transform.SetParent (parent, false);
		copy.name = obj.name;
		return copy;
	}
	
	public int ToHashCode (string ori)
	{
		return ori.GetHashCode ();
	}
	
	public bool ObjectIsNULL (UnityEngine.Object go)
	{
		return go == null;
	}
	
	public bool SystemObjectIsNULL (object obj)
	{
		return obj == null;
	}
	
	public float GetUIActiveHeight (GameObject go)
	{
		UIRoot ur = NGUITools.FindInParents<UIRoot> (go.transform);
		return ur.activeHeight;
	}
	
	public Component FindCompInParents (GameObject go, System.Type type)
	{
		if (go == null)
			return null;
		
		Component comp = go.GetComponent (type);
		if (comp == null) {
			Transform t = go.transform.parent;
			
			while (t != null && comp == null) {
				comp = t.gameObject.GetComponent (type);
				t = t.parent;
			}
		}
		return comp;
	}

	public T GetOrAddComponent<T> (GameObject obj)where T:MonoBehaviour
	{
		T comp = obj.GetComponent<T> ();
		if (null == comp) {
			comp = obj.AddComponent<T> ();
		}
		return comp;
	}
}
