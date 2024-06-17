using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        // check if collision object has IHeatable interface
        IHeatable heatable = collision.gameObject.GetComponent<IHeatable>();
        if (heatable != null)
        {
            heatable.Heat();
        }
    }
}
