using System;
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
        Debug.Log("Trigger Enter");
        if (!cutBoard.FishIsSnapped) return;
        Debug.Log("FIshIsSnapped -- ");
        if (other.CompareTag("Collider1"))
        {
            Debug.Log("COllision 1 -- ");
            if (countCollider1 <= countCollider2 + 1) countCollider1++;
        }
        if (other.CompareTag("Collider2"))
        {
            Debug.Log("COllision 2 -- ");
            if (countCollider1 <= countCollider1 + 1) countCollider2++;
        }
        if (countCollider1 > count && countCollider2 > count)
        {
            if (!cutBoard.currentFish.IsUnityNull()) cutBoard.currentFish.IncrementCut();
            OnFishCut?.Invoke();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollision Enter");
        if (!cutBoard.FishIsSnapped) return;
        Debug.Log("FIshIsSnapped");
        if (collision.collider == collider1)
        {
            Debug.Log("COllision 1");
            if (countCollider1 <= countCollider2+1) countCollider1++;
        }
        if (collision.collider == collider2)
        {
            Debug.Log("COllision 2");
            if (countCollider1 <= countCollider1 + 1) countCollider2++;
        }
        if(countCollider1 > count && countCollider2 > count)
        {
            if (!cutBoard.currentFish.IsUnityNull()) cutBoard.currentFish.knifeCutCount++;
            OnFishCut?.Invoke();
        }
    }
}
