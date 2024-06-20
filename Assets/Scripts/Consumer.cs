using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Consumable consumable = other.GetComponent<Consumable>();
        consumable.Consume();
        Debug.Log("Enter trigger");
    }
}
