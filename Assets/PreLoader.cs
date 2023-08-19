using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PreLoader : MonoBehaviour
{
	public Image loadingBar;

	public Text loadingPText;

	private bool updatePText;

	public GameObject privacyPolicy;

	public GameObject loadingPanel;

	private IEnumerator Start()
	{
		Screen.sleepTimeout = -1;
		yield return new WaitForSeconds(1f);
		if (!PlayerPrefs.HasKey("PrivacyStatus"))
		{
			PlayerPrefs.SetInt("PrivacyStatus", 0);
		}
		if (PlayerPrefs.GetInt("PrivacyStatus") == 0)
		{
			privacyPolicy.SetActive(true);
			AdsCallingManager._Instance.HideBanner();
		}
		else
		{
			loadingPanel.SetActive(true);
			StartCoroutine("LoadLevel");
		}
	}

	public void AcceptButton()
	{
		privacyPolicy.SetActive(false);
		PlayerPrefs.SetInt("PrivacyStatus", 1);
		loadingPanel.SetActive(true);
		StartCoroutine("LoadLevel");
	}

	public void OpenPrivacy()
	{
		Application.OpenURL("https://sites.google.com/view/fun-games-lab/homehttps://sites.google.com/view/fun-games-lab/home");
	}

	private IEnumerator LoadLevel()
	{
		updatePText = true;
		loadingBar.DOFillAmount(1f, 5f).SetEase(Ease.Linear);
		yield return new WaitForSecondsRealtime(5f);
		Application.LoadLevel(1);
	}

	private void Update()
	{
		if (updatePText)
		{
			loadingPText.text = (loadingBar.fillAmount * 100f).ToString("0") + "%";
		}
	}
}
