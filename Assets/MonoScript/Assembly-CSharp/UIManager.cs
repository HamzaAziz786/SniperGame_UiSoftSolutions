using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class UIManager : MonoBehaviour
{	
	public RectTransform flare1;
	public RectTransform flare2;
	public Text TopBarTextName;
	public GameObject[] UnlockImages;
	public GameObject coinShoppanel;
	public GameObject SettingPanel;
	public GameObject Quitpanel;
	public GameObject Mainemnupanel;
	public GameObject Levelselecitionpanel;
	public GameObject Gunselectionpanel;
	public GameObject Loadingpanel;
	bool fromWeaponUpdatesButton=false;
	[Serializable]
	public struct GunSpecs
	{
		public string gunName;

		[Range(0f, 1f)]
		public float[] gunSpecs;
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static TweenCallback _003C_003E9__76_0;

		internal void _003CInitFinalLoading_003Eb__76_0()
		{
			UnityEngine.Object.FindObjectOfType<LevelLoader>().LoadLLevel();
		}
	}

	private bool updateLoadingPText;

	public Text wpGunNameText;

	public GameObject mainMenuItems;

	public GameObject weaponSelectionItems;

	public Image loadingBarImage;

	public Text loadingText;

	public AudioClip clickSound;

	private AudioSource clickAudioSource;

	public static bool fromNextButton;

	public static bool canUseEscape;

	[Header("WEAPON SELECTION ITEMS")]
	public Text priceText;

	public Text playerCashText;

	public GameObject buyButton;

	public GameObject nextButton;

	public Text lessCashPanelText;

	public static int weaponSelected;

	public GunSpecs[] gun_Specs;

	public Image[] specsBar;

	public Text[] specsPercentageText;

	public ShopableProduct[] purchaseable_Items;
	public GameObject[] Shopguns;
	[Header("SETTINGS ITEMS")]
	public GameObject autoShootEnabled;

	public GameObject autoShootDisabled;

	public Slider audioSlider;

	public Slider soundsSlider;

	public Slider senstivitySlider;

	public AudioMixer mixer;

	public AudioMixer soundsMixer;

	public GameObject[] qualityEnabledButtons;

	public static int rewardIndex;

	private void Awake()
	{
		Application.targetFrameRate = 300;
	}

	private void Start()
	{
		flare1.DOLocalMove(new Vector3(105,-237,6.5f),2).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
		flare2.DOLocalMove(new Vector3(105,-237,6.5f),2).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
		PlayerPrefs.SetInt("GameCoins",9999999);
		PlayerPrefs.SetString(Shopguns[0].name,"SOLD");
		PlayerPrefs.SetString(Shopguns[1].name,"SOLD");
		AudioListener.volume = 1f;
		Time.timeScale = 1f;
		playerCashText.text = PlayerPrefs.GetInt("GameCoins") + "$";
		clickAudioSource = GetComponent<AudioSource>();
		//InitPlayerPrefs();
	
		if (fromNextButton)
		{
			fromNextButton = false;
			NextButton();
		}
		showthisGun(PlayerPrefs.GetInt("LastSelectedGun"));
		audioSlider.value = PlayerPrefs.GetFloat("GameMusicPref", 1f);
		soundsSlider.value = PlayerPrefs.GetFloat("GameSoundsPref", 1f);
		senstivitySlider.value = PlayerPrefs.GetFloat("SenstivityPref", 0.5f);
		AdsCallingManager._Instance.ShowBanner();
	}

	private void Update()
	{
		if (updateLoadingPText)
		{
			loadingText.text = (loadingBarImage.fillAmount * 100f).ToString("0") + "%";
		}
	}

	public void NextButton()
	{
		if(Quitpanel.activeInHierarchy==true)
		{
		Quitpanel.SetActive(false);
		Mainemnupanel.SetActive(true);
		mainMenuItems.SetActive(true);
		TopBarTextName.text="MAIN MENU";
		weaponSelectionItems.SetActive(false);

		}
		else if(Mainemnupanel.activeInHierarchy==true)
		{
			Mainemnupanel.SetActive(false);
			Levelselecitionpanel.SetActive(true);
			TopBarTextName.text="LEVEL SELECTION";
			mainMenuItems.SetActive(false);	
			

		}
		else if(Levelselecitionpanel.activeInHierarchy==true)
		{
			
			Levelselecitionpanel.SetActive(false);
			Gunselectionpanel.SetActive(true);
			TopBarTextName.text="WEAPON SELECTION";
			weaponSelectionItems.SetActive(true);
			DisableLockImages();
		}
		else if(Gunselectionpanel.activeInHierarchy==true)
		{
			Gunselectionpanel.SetActive(false);
			Loadingpanel.SetActive(true);
		}
	}
	public void ActiveGunSelection()
	{
			Levelselecitionpanel.SetActive(false);
			Gunselectionpanel.SetActive(true);
			DisableLockImages();

			Mainemnupanel.SetActive(false);
			mainMenuItems.SetActive(false);	
			weaponSelectionItems.SetActive(true);
			TopBarTextName.text="WEAPON SELECTION";
	}

	public void PreviousButton()
	{
		if(coinShoppanel.activeInHierarchy || SettingPanel.activeInHierarchy)
		{
			if(coinShoppanel.activeInHierarchy)
			coinShoppanel.SetActive(false);
			else if( SettingPanel.activeInHierarchy)
			SettingPanel.SetActive(false);
			return;
		}
		if(Quitpanel.activeInHierarchy==true)
		{
		Quitpanel.SetActive(false);
		Mainemnupanel.SetActive(true);
		TopBarTextName.text="MAIN MENU";
		mainMenuItems.SetActive(true);
		
		weaponSelectionItems.SetActive(false);

		}
		else if(Mainemnupanel.activeInHierarchy==true)
		{
		   Quitpanel.SetActive(true);
		   TopBarTextName.text="GAME QUIT";
		   Mainemnupanel.SetActive(false);
		}
	  else	if(Levelselecitionpanel.activeInHierarchy==true)
		{
			Mainemnupanel.SetActive(true);
			TopBarTextName.text="MAIN MENU";
			Levelselecitionpanel.SetActive(false);
			mainMenuItems.SetActive(true);
		}
	else	if(Gunselectionpanel.activeInHierarchy==true)
		{
			Levelselecitionpanel.SetActive(true);
			TopBarTextName.text="LEVEL SELECTION";
			Gunselectionpanel.SetActive(false);
			weaponSelectionItems.SetActive(false);
		}
	}

	int Mygun=0;
	public void Buynow()
	{
		if (PlayerPrefs.GetInt("GameCoins") >= Mygun*3000)
		{
			PlayerPrefs.SetString(Shopguns[Mygun].name, "SOLD");
			PlayerPrefs.SetInt("LastSelectedGun", Mygun);
			PlayerPrefs.SetInt("GameCoins", PlayerPrefs.GetInt("GameCoins") - (Mygun*3000));
			playerCashText.text = PlayerPrefs.GetInt("GameCoins") + "$";
			buyButton.SetActive(false);
			nextButton.SetActive(true);
			DisableLockImages();
		}
	}

	public void showthisGun(int weaponIndex)
	{
		Mygun=weaponIndex;
		weaponSelected=Mygun;
		for (int i = 0; i < Shopguns.Length; i++)
		{
			if (i == weaponIndex)
			{
				Shopguns[i].gameObject.SetActive(true);
				 wpGunNameText.text = gun_Specs[i].gunName;
			}
			else
			{
				Shopguns[i].gameObject.SetActive(false);
			}
		}
		float Tempfillamount=(float) (weaponIndex+1)/7;

		for(int i=0;i<specsBar.Length;i++)
		{
			specsBar[i].DOFillAmount(Tempfillamount, 0.5f);
			specsPercentageText[i].text = (Tempfillamount * 100f).ToString("0") + "%";
		}
		
		if (PlayerPrefs.GetString(Shopguns[weaponIndex].name) == "SOLD")
		{
			buyButton.SetActive(false);
			nextButton.SetActive(true);
			PlayerPrefs.SetInt("LastSelectedGun", weaponIndex);
		}
		else
		{
			print("BC Idher hon");
			priceText.text=weaponIndex*3000+"$";
			buyButton.SetActive(true);
			nextButton.SetActive(false);
		} 
		

	//	WeaponSliderHandler(shopCounter);
	}

	public void LoadLevel()
	{
		
		canUseEscape = false;
		updateLoadingPText = true;
		Loadingpanel.SetActive(true);
		loadingBarImage.DOFillAmount(1f, 7f).OnComplete(loadgamescene);
	}
	public void loadgamescene()
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
	private void ActivateLastSelectedGun()
	{
		
	}

	public void RewardedVideo()
	{
		rewardIndex = 0;
	}

	public void RewardedVideoHandler(int index)
	{
		rewardIndex = index;
		AdsCallingManager._Instance.ShowRewardedVideo();
	}

	public void GiveReward()
	{
		PlayerPrefs.SetInt("GameCoins", PlayerPrefs.GetInt("GameCoins") + 5000);
		playerCashText.text = PlayerPrefs.GetInt("GameCoins").ToString() ?? "";
	}

	public void PlayClickSfx()
	{
		clickAudioSource.PlayOneShot(clickSound);
	}

	public void GameQuit()
	{
		Application.Quit();
	}

	public void VFXController(bool status)
	{
		PlayClickSfx();
	}

	public void AutoShootToggle(bool status)
	{
		PlayClickSfx();
		if (status)
		{
			autoShootEnabled.SetActive(false);
			autoShootDisabled.SetActive(true);
			PlayerPrefs.SetInt("AutoShootStatus", 0);
		}
		else
		{
			autoShootEnabled.SetActive(true);
			autoShootDisabled.SetActive(false);
			PlayerPrefs.SetInt("AutoShootStatus", 1);
		}
	}

	public void Unlock_AllGuns()
	{
		for (int i = 0; i < purchaseable_Items.Length; i++)
		{
			PlayerPrefs.SetString(purchaseable_Items[i].name, "SOLD");
			//VerifyPurchaseStatus();
		}
	}

	private void InitPlayerPrefs()
	{
		if (!PlayerPrefs.HasKey("AutoShootStatus"))
		{
			PlayerPrefs.SetInt("AutoShootStatus", 1);
		}
		if (PlayerPrefs.GetInt("AutoShootStatus") == 0)
		{
			AutoShootToggle(true);
		}
	}

	public void OpenExternalUrl(string btnName)
	{
		switch (btnName)
		{
		case "More":
			Application.OpenURL("https://play.google.com/store/apps/developer?id=Fun+Games+Lab");
			break;
		case "Share":
			//SocialNetworks.ShareURL("Commando Strike", "https://play.google.com/store/apps/details?id=com.fungames.fps.shootinggames.gungames.modernstrike");
			break;
		case "RateUS":
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.fungames.fps.shootinggames.gungames.modernstrike");
			break;
		}
	}

	public void LessCashWatchVideo()
	{
		rewardIndex = 1;
		AdsCallingManager._Instance.ShowRewardedVideo();
	}

	public void LessCashWatchVideoReward()
	{
		PreviousButton();
		PlayerPrefs.SetInt("GameCoins", PlayerPrefs.GetInt("GameCoins") + 1000);
		playerCashText.text = PlayerPrefs.GetInt("GameCoins").ToString() ?? "";
		lessCashPanelText.text = "Congrat's You Got 1000 Cash";
	}

	public void SpecsHandler(int gunIndex)
	{
		
	}

	public void DisableLockImages()
	{
		for(int i=0;i<Shopguns.Length;i++)
		{
			if(PlayerPrefs.GetString(Shopguns[i].name)=="SOLD")
			UnlockImages[i].SetActive(false);
		}
	}
	public void PurchaseGunWithInapps(int index)
	{
		if (index == 0)
		{
			InappPurchaseManager.instace.PurchaseHandler("PurchasePDR");
		}
	}

	public void InappsPurchaseHandler(string inappName)
	{
		if (inappName == null)
		{
			return;
		}
		switch (inappName)
		{
		case "Unlock All Game":
		{
			PlayerPrefs.SetInt("RemoveAdS", 1);
			PlayerPrefs.SetString("Mode_3", "Unlocked");
			PlayerPrefs.SetString("Mode_5", "Unlocked");
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_1", 20);
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_3", 20);
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_5", 20);
			for (int j = 0; j < purchaseable_Items.Length; j++)
			{
				PlayerPrefs.SetString(purchaseable_Items[j].gameObject.name, "SOLD");
			}
			break;
		}
		case "Unlock All Levels":
			PlayerPrefs.SetString("Mode_3", "Unlocked");
			PlayerPrefs.SetString("Mode_5", "Unlocked");
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_1", 20);
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_3", 20);
			PlayerPrefs.SetInt("MaxLevelCleared_Mode_5", 20);
			break;
		case "Unlock All Weapons":
		{
			for (int i = 0; i < purchaseable_Items.Length; i++)
			{
				PlayerPrefs.SetString(purchaseable_Items[i].gameObject.name, "SOLD");
			}
			//VerifyPurchaseStatus();
			break;
		}
		}
	}

	public void Volume_Controller(float val)
	{
		mixer.SetFloat("MusicVol", Mathf.Log10(val) * 20f);
		PlayerPrefs.SetFloat("GameMusicPref", val);
	}

	public void Sounds_Controller(float val)
	{
		soundsMixer.SetFloat("MusicVol", Mathf.Log10(val) * 20f);
		PlayerPrefs.SetFloat("GameSoundsPref", val);
	}

	public void Senstivity_Controller(float val)
	{
		PlayerPrefs.SetFloat("SenstivityPref", val);
	}
	public void SetGameQuality(int index)
	{
		PlayClickSfx();
		for (int i = 0; i < qualityEnabledButtons.Length; i++)
		{
			qualityEnabledButtons[i].SetActive(false);
			if (i == index)
			{
				qualityEnabledButtons[i].SetActive(true);
			}
		}
	}
}
