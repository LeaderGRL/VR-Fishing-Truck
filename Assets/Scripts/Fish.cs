using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour, IHeatable
{
    public int heatTime { get; set; }
    public bool isCooked { get; set; }

    private Coroutine heatingCoroutine;

    void Start()
    {
        heatTime = 5;
        isCooked = false;
    }
    public void Heat()
    {
        Debug.Log("Fish is being heated : " + isCooked);

        if (!isCooked && heatingCoroutine == null)
        {
            heatingCoroutine = StartCoroutine(Heating());
            Debug.Log("Fish is being cooked");
        }
        else
        {
            Debug.Log("Fish is already cooked");
        }
    }

    private IEnumerator Heating()
    {
        Debug.Log("Fish is cooking");
        yield return new WaitForSeconds(heatTime);
        isCooked = true;
        heatingCoroutine = null; // Reset coroutine reference after heating is done
    }
}
