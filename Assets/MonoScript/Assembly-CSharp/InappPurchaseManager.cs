using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class InappPurchaseManager : MonoBehaviour, IStoreListener
{
	public static InappPurchaseManager instace;

	private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	public static string Product_TestPurchase = "android.test.purchased";

	public static string Product_Level1to10 = "level1to10";

	public static string Product_Level11to20 = "level11to20";

	public static string Product_Level21to30 = "level21to30";

	public static string Product_Stars100 = "stars100";

	public static string Product_Stars300 = "stars300";

	public static string Product_RemoveAds = "remove_ads";

	public static string Product_UnlockAllGuns = "unlock_allguns";

	public static string Product_UnlockAllLevels = "unlock_alllevels";

	public static string Product_UnlockAllGame = "unlock_allgame";

	public static string Product_BuyCoinInapp1 = "buy_coinpack1";

	public static string Product_BuyCoinInapp2 = "buy_coinpack2";

	public static string Product_BuyCoinInapp3 = "buy_coinpack3";

	public static string Product_BuyCoinInapp4 = "buy_coinpack4";

	public static string Product_BuyCoinInapp5 = "buy_coinpack5";

	public static string Product_BuyCoinInapp6 = "buy_coinpack6";

	public static string Product_GrenadeInapp1 = "buy_grenadepack1";

	public static string Product_GrenadeInapp2 = "buy_grenadepack2";

	public static string Product_GrenadeInapp3 = "buy_grenadepack3";

	public static string Product_GrenadeInapp4 = "buy_grenadepack4";

	public static string Product_GrenadeInapp5 = "buy_grenadepack5";

	public static string Product_GrenadeInapp6 = "buy_grenadepack6";

	public static string Product_HealthKit1Inapp = "buy_healthkitpack1";

	public static string Product_HealthKit2Inapp = "buy_healthkitpack2";

	public static string Product_HealthKit3Inapp = "buy_healthkitpack3";

	public static string Product_HealthKit4Inapp = "buy_healthkitpack4";

	public static string Product_HealthKit5Inapp = "buy_healthkitpack5";

	public static string Product_HealthKit6Inapp = "buy_healthkitpack6";

	public static string Product_PDR = "buy_pdr";

	public static string Product_CQ16 = "buy_cq";

	public static string Product_CARBINE = "buy_carbine";

	public static string Product_SNIPER = "buy_sniper";

	public static string Product_MACHINEGUN = "buy_machinegun";

	public static string Product_InappPack1 = "inapp_pack1";

	public static string Product_InappPack2 = "inapp_pack2";

	public static string Product_InappPack3 = "inapp_pack3";

	public static string Product_InappPack4 = "inapp_pack4";

	public static string Product_InappPack5 = "inapp_pack5";

	public static string Product_InappPack6 = "inapp_pack6";

	public static string Product_Balls5 = "balls5";

	private void Start()
	{
		instace = this;
		if (m_StoreController == null)
		{
			InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (!IsInitialized())
		{
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			configurationBuilder.AddProduct(Product_TestPurchase, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_RemoveAds, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_UnlockAllGuns, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_UnlockAllLevels, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_UnlockAllGame, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp1, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp2, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp3, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp4, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp5, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_GrenadeInapp6, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit1Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit2Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit3Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit4Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit5Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_HealthKit6Inapp, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp1, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp2, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp3, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp4, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp5, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_BuyCoinInapp6, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_PDR, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_CQ16, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_CARBINE, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_SNIPER, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_MACHINEGUN, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack1, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack2, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack3, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack4, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack5, ProductType.NonConsumable);
			configurationBuilder.AddProduct(Product_InappPack6, ProductType.NonConsumable);
			UnityPurchasing.Initialize(this, configurationBuilder);
		}
	}

	private bool IsInitialized()
	{
		if (m_StoreController != null)
		{
			return m_StoreExtensionProvider != null;
		}
		return false;
	}

	public void PurchaseHandler(string btnName)
	{
		if (btnName != null)
		{
			switch (btnName)
			{
			case "RemoveAds":
				BuyProductID(Product_RemoveAds);
				break;
			case "Unlock All Game":
				BuyProductID(Product_UnlockAllGame);
				break;
			case "Unlock All Levels":
				BuyProductID(Product_UnlockAllLevels);
				break;
			case "Unlock All Weapons":
				BuyProductID(Product_UnlockAllGuns);
				break;
			case "PurchasePDR":
				BuyProductID(Product_PDR);
				break;
			case "PurchaseCQ16":
				BuyProductID(Product_CQ16);
				break;
			case "PurchaseCARBINE":
				BuyProductID(Product_CARBINE);
				break;
			case "PurchaseSNIPER":
				BuyProductID(Product_SNIPER);
				break;
			case "PurchaseMACHINEGUN":
				BuyProductID(Product_MACHINEGUN);
				break;
			case "PurchaseHealthkit1":
				BuyProductID(Product_HealthKit1Inapp);
				break;
			case "PurchaseHealthkit2":
				BuyProductID(Product_HealthKit2Inapp);
				break;
			case "PurchaseHealthkit3":
				BuyProductID(Product_HealthKit3Inapp);
				break;
			case "PurchaseHealthkit4":
				BuyProductID(Product_HealthKit4Inapp);
				break;
			case "PurchaseHealthkit5":
				BuyProductID(Product_HealthKit5Inapp);
				break;
			case "PurchaseHealthkit6":
				BuyProductID(Product_HealthKit6Inapp);
				break;
			case "Cash 1 Inapp":
				BuyProductID(Product_BuyCoinInapp1);
				break;
			case "Cash 2 Inapp":
				BuyProductID(Product_BuyCoinInapp2);
				break;
			case "Cash 3 Inapp":
				BuyProductID(Product_BuyCoinInapp3);
				break;
			case "Cash 4 Inapp":
				BuyProductID(Product_BuyCoinInapp4);
				break;
			case "Cash 5 Inapp":
				BuyProductID(Product_BuyCoinInapp5);
				break;
			case "Cash 6 Inapp":
				BuyProductID(Product_BuyCoinInapp6);
				break;
			case "Explosive 1 Inapp":
				BuyProductID(Product_GrenadeInapp1);
				break;
			case "Explosive 2 Inapp":
				BuyProductID(Product_GrenadeInapp2);
				break;
			case "Explosive 3 Inapp":
				BuyProductID(Product_GrenadeInapp3);
				break;
			case "Explosive 4 Inapp":
				BuyProductID(Product_GrenadeInapp4);
				break;
			case "Explosive 5 Inapp":
				BuyProductID(Product_GrenadeInapp5);
				break;
			case "Explosive 6 Inapp":
				BuyProductID(Product_GrenadeInapp6);
				break;
			case "GunPack1":
				BuyProductID(Product_InappPack1);
				break;
			case "GunPack2":
				BuyProductID(Product_InappPack2);
				break;
			case "GunPack3":
				BuyProductID(Product_InappPack3);
				break;
			case "GunPack4":
				BuyProductID(Product_InappPack4);
				break;
			case "GunPack5":
				BuyProductID(Product_InappPack5);
				break;
			case "GunPack6":
				BuyProductID(Product_InappPack6);
				break;
			}
		}
	}

	private void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("OnInitialized: PASS");
		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (!string.Equals(args.purchasedProduct.definition.id, Product_TestPurchase, StringComparison.Ordinal))
		{
			if (string.Equals(args.purchasedProduct.definition.id, Product_RemoveAds, StringComparison.Ordinal))
			{
				PlayerPrefs.SetInt("RemoveAdS", 1);
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_UnlockAllGuns, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Unlock All Weapons");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_UnlockAllLevels, StringComparison.Ordinal))
			{
				if (UnityEngine.Object.FindObjectOfType<UIManager>() != null)
				{
					UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Unlock All Levels");
				}
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_UnlockAllGame, StringComparison.Ordinal))
			{
				PlayerPrefs.SetString("AllGameUnlocked", "Purchased");
				/* if (UnityEngine.Object.FindObjectOfType<SplashSceneController>() != null)
				{
					UnityEngine.Object.FindObjectOfType<SplashSceneController>().UnlockAllGame();
				} */
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp1, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 1 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp2, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 2 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp3, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 3 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp4, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 4 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp5, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 5 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_BuyCoinInapp6, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Cash 6 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp1, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp1");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp2, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp2");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp3, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp3");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp4, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp4");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp5, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp5");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_GrenadeInapp6, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Grenade Inapp6");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit1Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 1 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit2Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 2 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit3Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 3 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit4Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 4 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit5Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 5 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_HealthKit6Inapp, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Health 6 Inapp");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_PDR, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase PDR");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_CQ16, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase CQ16");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_CARBINE, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase CARBINE");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_SNIPER, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase SNIPER");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_MACHINEGUN, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase MACHINEGUN");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack1, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack1");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack2, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack2");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack3, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack3");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack4, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack4");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack5, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack5");
			}
			else if (string.Equals(args.purchasedProduct.definition.id, Product_InappPack6, StringComparison.Ordinal))
			{
				UnityEngine.Object.FindObjectOfType<UIManager>().InappsPurchaseHandler("Purchase InappPack6");
			}
			else
			{
				Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			}
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}
