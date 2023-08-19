using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHandle : MonoBehaviour
{   
    public static UIHandle instance;
    public GameObject AISpawner;
    public GameObject miniMap;
    public GameObject player;
    public Transform[] playerPositions;
    public PlayerWeapons playerWeapons;
    public Text levelno;
    public Text NPCDeathText;
    [HideInInspector]
    public int npcDeathcount;
    public GameObject AFtoggleOn,AFtoggleOff;
    bool isAutoFireEnabled=false;
    public GameObject Pausepanel;
    public GameObject completePanel;
    public GameObject completestarspanel;
    public GameObject failedpanel;
    [HideInInspector]
    public int MaxenemyCount=0;
    public GameObject tempcam;

    void Awake()
    {
        instance=this;
        AudioListener.volume=1;
        AudioListener.pause=false;
        miniMap.SetActive(true);
        playerWeapons.firstWeapon=UIManager.weaponSelected+1;
         int rand=Random.Range(0,playerPositions.Length);
     player.transform.position=playerPositions[rand].position;
       player.transform.rotation=playerPositions[rand].rotation;
       player.SetActive(true);
        AISpawner.SetActive(true);        
        MaxenemyCount=LevelSystemHandler.levelSelected+7;                               
        levelno.text="Level "+LevelSystemHandler.levelSelected;
        NPCDeathText.text=npcDeathcount+"/"+(MaxenemyCount+1);
    }
    // Start is called before the first frame update
    void Start()
    {   
        Application.targetFrameRate = 300;
        
    }

    public void PauseButton()
    {
        Time.timeScale=0;
        Pausepanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale=1;
        Pausepanel.SetActive(false);
    }
    public void Restart()
    {
        Time.timeScale=1;
        Application.LoadLevel(Application.loadedLevel);
    }
    public void Next()
    {
        Time.timeScale=1;
        LevelSystemHandler.levelSelected++;
         Application.LoadLevel(Application.loadedLevel);
    }

    public void Home()
    {
        Time.timeScale=1;
        Application.LoadLevel(1);
    }
    public void UpdateNPCDeath()
    {
        if(npcDeathcount<MaxenemyCount)
        {
        npcDeathcount++;
        NPCDeathText.text=npcDeathcount+"/"+(MaxenemyCount+1);
        }
        else
        {
          StartCoroutine(levelCompleted());
        }
    }
    IEnumerator levelCompleted()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        completestarspanel.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        completestarspanel.SetActive(false);
        completePanel.SetActive(true);
        if(LevelSystemHandler.levelSelected >=  PlayerPrefs.GetInt("MaxLevelCleared_Mode_"+LevelSystemHandler.modeSelected))
        PlayerPrefs.SetInt("MaxLevelCleared_Mode_"+LevelSystemHandler.modeSelected,LevelSystemHandler.levelSelected);
        AudioListener.volume=0;
        if(PlayerPrefs.GetFloat("PlayerRankFillBar")<1)
        PlayerPrefs.SetFloat("PlayerRankFillBar",PlayerPrefs.GetFloat("PlayerRankFillBar")+0.2f);
        else
        PlayerPrefs.SetFloat("PlayerRankFillBar",0.2f);
        
    }
    public void autoFireToggle()
    {
        if(isAutoFireEnabled)
        {
            isAutoFireEnabled=false;
            AFtoggleOn.SetActive(false);
            AFtoggleOff.SetActive(true);
            AutoFire.isAutoFire=false;
        }
        else
        {

            isAutoFireEnabled=true;
            AFtoggleOn.SetActive(true);
            AFtoggleOff.SetActive(false);
            AutoFire.isAutoFire=true;
        }
    }
    public void LevelFailed()
    {
        failedpanel.SetActive(true);
        AudioListener.volume=1;
        tempcam.SetActive(true);
    }
}
