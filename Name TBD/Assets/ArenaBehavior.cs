using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehavior : MonoBehaviour {

    [SerializeField]
    float ringFlashTime, timeToDrop;

    [SerializeField]
    List<GameObject> arenaRings;

    Color aboutToDrop = Color.red;

    int ringToDrop;
    float timer;

    void Start () {
        timer = 0;
        ringToDrop = arenaRings.Count - 1;
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
            }
        }
    }

    private IEnumerator Wait(int ringToDrop)
    {
        yield return new WaitForSeconds(ringFlashTime);
        arenaRings[ringToDrop].SetActive(false);
        arenaRings.Remove(arenaRings[ringToDrop]);
    }
}
