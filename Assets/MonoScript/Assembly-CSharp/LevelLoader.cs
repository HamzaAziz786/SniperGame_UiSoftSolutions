using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	public static int SceneNo;

	private void Awake()
	{
		Screen.sleepTimeout = -1;
		Time.timeScale = 1f;
	}

	public void LoadLLevel()
	{
		AudioListener.volume = 0f;
		switch (LevelSystemHandler.modeSelected)
		{
		case 0:
			Application.LoadLevel(2);
			break;
		case 1:
			Application.LoadLevel(3);
			break;
		case 2:
			Application.LoadLevel(4);
			break;
		}
	}
}
