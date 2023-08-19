using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSystemHandler : MonoBehaviour
{
	public GameObject[] levelButtons;

	public static int modeSelected;

	public static int levelSelected;

	public int lockImageIndex;

	public Transform selectorImage;

	public Scrollbar levelsScrollBar;

	private void OnEnable()
	{
		//PlayerPrefs.SetInt("MaxLevelCleared_Mode_0", 11);
		//PlayerPrefs.SetInt("MaxLevelCleared_Mode_1", 7);
		InitLevelsData();
	}

	public void SelectMode(int index)
	{
		modeSelected = index;
	}

	public void SelectLevel()
	{
		levelSelected = int.Parse(EventSystem.current.currentSelectedGameObject.name);
		Object.FindObjectOfType<UIManager>().NextButton();
	}

	private void InitLevelsData()
	{
		for (int i = 0; i < levelButtons.Length; i++)
		{
			levelButtons[i].transform.GetChild(lockImageIndex).gameObject.SetActive(true);
			levelButtons[i].GetComponent<Button>().interactable=false;
			levelButtons[i].transform.GetChild(1).gameObject.SetActive(false);
			levelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
			levelButtons[i].transform.GetChild(3).gameObject.SetActive(false);
		}
		for (int j = 0; j <= PlayerPrefs.GetInt("MaxLevelCleared_Mode_" + modeSelected); j++)
		{
			levelButtons[j].transform.GetChild(lockImageIndex).gameObject.SetActive(false);
			levelButtons[j].GetComponent<Button>().interactable=true;
			if (j < PlayerPrefs.GetInt("MaxLevelCleared_Mode_" + modeSelected))
			{
				levelButtons[j].transform.GetChild(1).gameObject.SetActive(true);
				levelButtons[j].transform.GetChild(2).gameObject.SetActive(true);
				levelButtons[j].transform.GetChild(3).gameObject.SetActive(true);
			}
			if (j == PlayerPrefs.GetInt("MaxLevelCleared_Mode_" + modeSelected))
			{
				selectorImage.transform.localPosition = levelButtons[j].transform.localPosition;
			}
		}
	}

	public void SetScrollbarVal()
	{
	}
}
