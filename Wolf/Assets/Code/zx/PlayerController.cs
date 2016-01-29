using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject effectTarget;

	void Start ()
	{

	}

	void OnDestroy ()
	{

	}

	[ContextMenu("CheckAnim")]
	public void CheckAnim ()
	{
		SimpleAnimatorPlayer simp = Utils.Instance.GetOrAddComponent<SimpleAnimatorPlayer> (effectTarget);
		simp.animatorHelper.Play ("New Animation", 1);
//		simp.animatorHelper.loopCountChangedListener = _animChange;
		simp.animatorHelper.stateChangedListener = _animChange;
	}

	void _animChange (AnimatorStateInfo oldState, AnimatorStateInfo newState)
	{
		if (oldState.IsName ("New Animation")) {
			print ("lallalallalalallalal");
		}
	}
}
