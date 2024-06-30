using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttedFish : MonoBehaviour, IHeatable
{
    [SerializeField] private int heatTime;
    public int HeatTime { get; set; }
    public bool IsCooked { get; set; }

    private Coroutine heatingCoroutine;

    [SerializeField] private MeshRenderer[] meshs;
    [SerializeField] private Material[] rawMat;
    [SerializeField] private Material[] cookedMat;

    void Start()
    {
        HeatTime = heatTime;
        IsCooked = false;
        foreach(var mesh in meshs)
        {
            mesh.material = rawMat[Random.Range(0, 2)];
        }
    }

    public void Heat()
    {
        Debug.Log("Fish is being heated : " + IsCooked);

        if (!IsCooked && heatingCoroutine == null)
        {
            heatingCoroutine = StartCoroutine(Heating());
        }
    }

    private IEnumerator Heating()
    {
        Debug.Log("Fish is cooking");
        yield return new WaitForSeconds(HeatTime);
        Debug.Log("Fish is cooked");
        IsCooked = true;
        foreach (var mesh in meshs)
        {
            mesh.material = cookedMat[Random.Range(0, 2)];
        }
        heatingCoroutine = null; // Reset coroutine reference after heating is done
    }
}
