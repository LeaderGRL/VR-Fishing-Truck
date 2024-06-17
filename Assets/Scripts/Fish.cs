using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour, IHeatable
{
    public int heatTime { get; set; }
    public bool isCooked { get; set; }

    public void Heat()
    {
        Debug.Log("Fish is being heated");

        if (!isCooked)
        {
            StartCoroutine(Heating());
        }
    }

    private IEnumerator Heating()
    {
        isCooked = true;
        Debug.Log("Fish is cooked");
        yield return new WaitForSeconds(heatTime);
        isCooked = false;
    }

    void Start()
    {
        heatTime = 10;
        isCooked = false;
    }


}
