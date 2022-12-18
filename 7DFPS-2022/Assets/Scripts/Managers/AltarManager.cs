using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarManager : MonoBehaviour
{
    public static AltarManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Yo you got more than one altar manager");
        }
        instance = this;
    }

    public GameObject[] altars;

    public void ActivateNewAltar()
    {
        //int altarNum = Random.Range(0, 4);

        int altarNum = 0;

        //get altar that is furthest from player
        float maxDistance = 0;
        for (int i = 0; i < altars.Length; i++)
        {
            if (Vector3.Distance(Player.Instance.transform.position, altars[i].transform.position) > maxDistance)
            {
                maxDistance = Vector3.Distance(Player.Instance.transform.position, altars[i].transform.position);
                altarNum = i;
            }
        }

        altars[altarNum].GetComponent<AltarBehavior>().Activate();
        Debug.Log("altar activated");
    }


}
