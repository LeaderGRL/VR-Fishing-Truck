using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttedFish : MonoBehaviour, IHeatable
{
    [SerializeField] private int heatTime;
    public int HeatTime { get; set; }
    public bool IsCooked { get; set; }

    private Coroutine heatingCoroutine;

    void Start()
    {
        HeatTime = heatTime;
        IsCooked = false;
    }

    public void Heat()
    {
        Debug.Log("Fish is being heated : " + IsCooked);

        if (!IsCooked && heatingCoroutine == null)
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
        yield return new WaitForSeconds(HeatTime);
        IsCooked = true;
        heatingCoroutine = null; // Reset coroutine reference after heating is done
    }
}
