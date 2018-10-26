using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehavior : MonoBehaviour {

    [SerializeField]
    float ringFlashTime, timeToDrop;

    [SerializeField]
    List<GameObject> arenaRings;

    int ringToDrop;
    float timer;

    private void Awake()
    {
        timer = 0;
        ringToDrop = arenaRings.Count - 1;
        SetArenaBounds();
    }

    void Update()
    {
        if (arenaRings.Count > 1)
        {
            timer += Time.deltaTime;

            if (timer > timeToDrop && ringToDrop >= 0) //timer for rings falling
            {
                timer = 0;

                arenaRings[ringToDrop].GetComponent<ArenaFall>().Fall();
                StartCoroutine(Wait(ringToDrop));
                ringToDrop--;
                SetArenaBounds();
            }
        }
    }

    private IEnumerator Wait(int ringToDrop)
    {
        yield return new WaitForSeconds(ringFlashTime);
        arenaRings[ringToDrop - 1].tag = "ArenaEdge";
        GameManager.getInstance().SetArenaFell(true);
        arenaRings[ringToDrop].SetActive(false);
        arenaRings.Remove(arenaRings[ringToDrop]);
    }


    public void SetArenaBounds()
    {
        GameManager.getInstance().SetCurrentEdge(arenaRings[ringToDrop]);
    }
}
