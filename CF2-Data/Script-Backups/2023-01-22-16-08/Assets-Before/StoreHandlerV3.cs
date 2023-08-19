using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHandlerV3 : MonoBehaviour
{   
    public GameObject[] uIPanels;
    public GameObject[] extraPanels;
    public GameObject[] extraObjects;
    public PurchaseAbleItem[] purchaseable_Items;
    public GameObject[] specs_Items;
    int uICounter=1;
    int shopCounter=0;
    int dummyVal=0;
    private PurchaseAbleItem selectedItem;
    GameObject extraPanel;
    public GameObject priceBG,lessCashPanel;
    public GameObject buyButton,nextButton;
    public Text playerCashText,priceText;
    public static int itemSelected=0;
    //UIHandlerV3 uIHandler;
    void Start()
    {   
        PlayerPrefs.SetInt("PlayerCash",5000000);
        playerCashText.text=PlayerPrefs.GetInt("PlayerCash").ToString();
        if(purchaseable_Items.Length>0)
        PlayerPrefs.SetString(purchaseable_Items[0].name,"Purchased");
    }
    public void Next()
    {
        if(uICounter<uIPanels.Length-1)
        {
            uIPanels[uICounter].SetActive(false);
            uICounter++;
            uIPanels[uICounter].SetActive(true);
            ExtraFunctionAlityHandler(true);
        }
    }
    public void Previous()
    {
        if(extraPanel==null)
        {
        if(uICounter>0)
        {
            uIPanels[uICounter].SetActive(false);
            uICounter--;
            uIPanels[uICounter].SetActive(true);
            ExtraFunctionAlityHandler(false);
        }
        else
        {
            Next();
        }
        }
        else
        {
            extraPanel.SetActive(false);
            extraPanel=null;
        }
    }
    void ExtraFunctionAlityHandler(bool direction)
    {
        switch (uICounter)
        {
            case 0:
            break;
            case 1:
            if(!direction)
            {
            extraObjects[0].SetActive(true);
            extraObjects[1].SetActive(false);
            extraObjects[2].SetActive(false);
            }
            break;
            case 2:
            if(direction)
            {
            extraObjects[0].SetActive(false);
            extraObjects[1].SetActive(true);
            extraObjects[2].SetActive(true);
            }
            break;
            case 3:
            break;
            case 4:
            break;
        }
    }
    public void ActivateExtraPanel(int index)
    {
        extraPanel=extraPanels[index];
        extraPanel.SetActive(true);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        Previous();
    }
    public void ShopItems_Navigation(string direction)
    {
        switch(direction)
        {
            case "Right":
            if(shopCounter==purchaseable_Items.Length-1)
            {
             SwapItem(-(purchaseable_Items.Length-1));
             shopCounter=0;
            }
            else
            {
             SwapItem(1);
             shopCounter++;
            }
            break;
            //
            case "Left":
            if(shopCounter==0)
            {
            SwapItem(purchaseable_Items.Length-1);
            shopCounter=purchaseable_Items.Length-1;
            }
            else
            {
            SwapItem(-1);
            shopCounter--;  
            }
            break;
        }
    }
   public void SwapItem(int val)
    {
        purchaseable_Items[shopCounter].gameObject.SetActive(false);
        purchaseable_Items[shopCounter+val].gameObject.SetActive(true);
       // specs_Items[shopCounter].gameObject.SetActive(false);
       // specs_Items[shopCounter+val].gameObject.SetActive(true);
        selectedItem=purchaseable_Items[shopCounter+val];
        dummyVal=shopCounter+val;
        PurchaseCheck();
    }
    public void DirectGunSelectHandler(int index)
    {   
        if(index>dummyVal)
        {
         int temp=index-dummyVal;
         for(int i=0;i<temp;i++)
         ShopItems_Navigation("Right");
        }
        else if(index<dummyVal)
        {
        int temp=dummyVal-index;
         for(int i=0;i<temp;i++)
         ShopItems_Navigation("Left");
        }
    }
    public void WeaponSelector(int weaponIndex)
    {
        for(int i=0;i<purchaseable_Items.Length;i++)
        {
            if(i!=weaponIndex)
            {
            purchaseable_Items[i].gameObject.SetActive(false);
            }
            else
            {
            purchaseable_Items[i].gameObject.SetActive(true);
            }
        }
        shopCounter=weaponIndex;
        dummyVal=weaponIndex;
        selectedItem=purchaseable_Items[shopCounter];
        PurchaseCheck();
    }
     public void Buy()
    {   
        if(PlayerPrefs.GetInt("PlayerCash")>=selectedItem.item_Price)
        {
            PlayerPrefs.SetString(selectedItem.name,"Purchased");
            PlayerPrefs.SetInt("PlayerCash",PlayerPrefs.GetInt("PlayerCash")-selectedItem.item_Price);
            playerCashText.text=PlayerPrefs.GetInt("PlayerCash")+"$";
            PurchaseCheck();
        }
        else
        {
        ActivateExtraPanel(1);
        }
    }
    void PurchaseCheck()
    {
    if(PlayerPrefs.GetString(selectedItem.name)=="Purchased")
    {
       priceBG.SetActive(false);
        buyButton.SetActive(false);
        nextButton.SetActive(true);
        itemSelected=dummyVal;
      //  PlayerPrefs.SetInt("LastSelectedProduct_"+UIHandlerV3.modeSelected,dummyVal);
    }
    else
    {   
       priceText.text=selectedItem.item_Price.ToString();
       priceBG.SetActive(true);
       buyButton.SetActive(true);
       nextButton.SetActive(false);
    }
    }
    public void LoadLevel()
    {
        Application.LoadLevel(1);
    }
}
