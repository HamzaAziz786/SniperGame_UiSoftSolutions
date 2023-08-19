using UnityEngine;

public class AdsCallingManager : MonoBehaviour
{
	public static AdsCallingManager _Instance;

	private void Awake()
	{
		if (_Instance == null)
		{
			_Instance = this;
		}
		else if (_Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void ShowBanner()
	{
	}

	public void ShowBannerTopLeft()
	{
	}

	public void ShowBannerBottom()
	{
	}

	public void HideBanner()
	{
	}

	public void ShowInterstitialAD()
	{
	}

	public void ShowRewardedVideo()
	{
	}
}
