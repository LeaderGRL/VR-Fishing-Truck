using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeCut : MonoBehaviour
{
    [SerializeField] private CutBoard cutBoard;
    [SerializeField] private BoxCollider collider1;
    [SerializeField] private BoxCollider collider2;

    private int countCollider1 = 0;
    private int countCollider2 = 0;
    private int count = 0;
    public event Action OnFishCut;


    private void OnTriggerEnter(Collider other)
    {
        if (!cutBoard.FishIsSnapped) { Debug.Log("Trigger Knife : FIshIsSnapped"); return; }
        Debug.Log("Trigger Knife : FIshIsSnapped");
        if (other.CompareTag("Collider1"))
        {
            countCollider1++;
            StartCoroutine(WaitThenResetCount());
        }
        if (other.CompareTag("Collider2"))
        {
            if(countCollider1 >= 1)
            countCollider2++;
        }
        if (countCollider1 > 1 && countCollider2 > 1)
        {
            if (!cutBoard.currentFish.IsUnityNull()) cutBoard.currentFish.IncrementCut();
            //OnFishCut?.Invoke();
            //count++;
            countCollider1 = 0;
            countCollider2 = 0;
            StopCoroutine(WaitThenResetCount());
        }
    }

    private IEnumerator WaitThenResetCount()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("reset knife cut count");
        countCollider1 = 0;
        countCollider2 = 0;
    }
}
