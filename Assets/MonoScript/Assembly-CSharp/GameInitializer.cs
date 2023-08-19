using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	public enum InitializationCategory
	{
		AI = 0,
		Levels = 1,
		Player = 2,
		ClanMode = 3,
		PVPMode = 4,
		QuickPlay = 5,
		Weapon = 6
	}

	public InitializationCategory myCategory;

	public void InitAI()
	{
	}

	public void InitLevels()
	{
	}

	public void InitPlayer()
	{
	}

	public void InitClanMode()
	{
	}

	public void InitPVP()
	{
	}

	public void InitQuickPlay()
	{
	}

	public void InitWeapons()
	{
	}
}
