using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCut : MonoBehaviour
{
    [SerializeField] private BoxCollider collider1;
    [SerializeField] private BoxCollider collider2;

    private int countCollider1 = 0;
    private int countCollider2 = 0;

    public event Action OnFishCut;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == collider1)
        {
            if(countCollider1 <= countCollider2+1) countCollider1++;
        }
        if (collision.collider == collider2)
        {
            if (countCollider1 <= countCollider1 + 1) countCollider2++;
        }
        if(countCollider1 > 3 && countCollider2 > 3)
        {
            OnFishCut?.Invoke();
        }
    }
}
