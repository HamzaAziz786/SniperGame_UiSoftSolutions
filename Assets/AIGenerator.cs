using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject[] aiSoldiers;
    public Transform[] spawnPoint;
    GameObject TempAIStoreage;
    int aiIndex=0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnAI();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnAI();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnAI();
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnAI();
    }

    public void SpawnAI()
    {
        if(UIHandle.instance.npcDeathcount<(UIHandle.instance.MaxenemyCount-3))
        {
        if(aiIndex<spawnPoint.Length-1)
        {
        int rand=Random.Range(0,aiSoldiers.Length);
        TempAIStoreage=Instantiate(aiSoldiers[rand],spawnPoint[aiIndex].transform.position,spawnPoint[aiIndex].transform.rotation).gameObject;
        aiIndex++;
        }
        else
        {
            int rand=Random.Range(0,aiSoldiers.Length);
            TempAIStoreage=Instantiate(aiSoldiers[rand],spawnPoint[0].transform.position,spawnPoint[0].transform.rotation).gameObject;
            aiIndex=0;
        }
        TempAIStoreage.SetActive(true);
        }
    }
}
