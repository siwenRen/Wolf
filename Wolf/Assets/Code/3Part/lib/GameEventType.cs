using UnityEngine;
using System.Collections;

public static class GameEventType
{
	//game state
	public static string GameStartEvent = "GameStartEvent";
	public static string GamePlay = "GameEventType_GamePlay";
	public static string GameFail = "Game_Fail";
	public static string GameWin = "Game_Win";
	//
	public const string CameraRayCastHit = "Game_CameraRayCastHit";
	public const string NormalAttack = "Game_NormalAttack";
	public const string UseSkill = "Game_UseSkill";
	public const string SealBreak = "Game_SealBreak";
	//shiyong fengyin
	public const string UseSeal = "Game_UseSeal";
	public const string RepairSeal = "Game_RepairSeal";
	//
	public const string TriggerDragSkill = "Game_TriggerDragSkill";
	public const string TriggerClickSkill = "Game_TriggerClickSkill";
	//skill
	public const string SkillDragBanKuai = "Game_SkillDragBanKuai";
	public const string SkillSlowDown = "Game_SkillSlowDown";
	public const string SealDragBanKuai = "Game_SealDragBanKuai";
}
