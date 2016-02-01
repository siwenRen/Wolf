using UnityEngine;
using System.Collections;
using System;

public class GameProgress : SingleTonGO<GameProgress>
{
	public enum GameState
	{
		Logo,
		WaittingStart,
		Playing,
		Lose,
		Win,
		LunHui,
	}
	GameState laststate;
	public GameState nowstate = GameState.Logo;
	public UIWidget mask;
	public GameObject lunhuiButton;
	bool islock;

	void Start ()
	{
		InitEvent ();

		Logo ();
	}

	void InitEvent ()
	{
		// lunhui
		UIEventListener.Get (lunhuiButton).onClick = (go) => {
			// zhixing lunhui
			nowstate = GameState.LunHui;
		};
	}

	public void Lock ()
	{
		islock = true;
		mask.gameObject.SetActive (true);
	}

	public void UnLock ()
	{
		islock = true;
		mask.gameObject.SetActive (true);
	}

	void Update ()
	{
		if (laststate != nowstate) {
			switch (nowstate) {
			case GameState.Logo:
				Logo ();
				break;
			case GameState.WaittingStart:
				WaittingStart ();
				break;
			case GameState.Playing:
				Playing ();
				break;
			case GameState.Lose:
				Lose ();
				break;
			case GameState.Win:
				Win ();
				break;
			case GameState.LunHui:
				LunHui ();
				break;
			}
			laststate = nowstate;
		}
	}

	void Logo ()
	{
		LogoAnim.Me.Play (() => {
			nowstate = GameState.WaittingStart;
		});
	}

	void WaittingStart ()
	{
		Messenger.Broadcast (GameEventType.GameStartEvent);

		WaittingStartAnim.Me.Play (() => {
			// startGame
			if (nowstate == GameState.WaittingStart) {
				GameStartAnim.Me.Play (() => {
					Messenger.Broadcast (GameEventType.GamePlay);
					nowstate = GameState.Playing;
				});
			}
		});
	}

	void Playing ()
	{

	}

	void Lose ()
	{
		GameEndAnim.Me.Play (() => {
			Application.LoadLevel (0);
//			nowstate = GameState.Logo;
		});
	}

	void Win ()
	{
		GameWinAnim.Me.Play (() => nowstate = GameState.LunHui);
	}

	void LunHui ()
	{
		GameLunHuiAnim.Me.Play (() => {
			nowstate = GameState.WaittingStart;
//			GameStartAnim.Me.Play (() => {
//				Messenger.Broadcast (GameEventType.GameStartEvent);
//				
//				nowstate = GameState.Playing;
//			});
		});
	}
}
