using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		if (null != animator) {
			SimpleAnimatorPlayer simp = Utils.Instance.GetOrAddComponent<SimpleAnimatorPlayer> (gameObject);
			simp.animatorHelper.stateChangedListener = _animChange;
		}
	}

	void _animChange (AnimatorStateInfo oldState, AnimatorStateInfo newState)
	{
		if (newState.IsName ("Empty")) {
			Destroy (gameObject);
		}
	}
}
