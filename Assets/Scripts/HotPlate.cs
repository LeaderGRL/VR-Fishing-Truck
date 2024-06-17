using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{
    private IHeatable heatable;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (heatable != null)
        {
            heatable.Heat();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HotPlate is heating up");

        // check if collision object has IHeatable interface
        heatable = collision.gameObject.GetComponent<IHeatable>();

        if (heatable != null)
        {
            Debug.Log("HotPlate detected a heatable object: " + collision.gameObject.name);
        }
        else
        {
            Debug.Log("Collision object does not have IHeatable interface: " + collision.gameObject.name);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("HotPlate is cooling down");

        // Check if the exiting object is the same as the current heatable object
        if (collision.gameObject.GetComponent<IHeatable>() == heatable)
        {
            heatable = null;
        }
    }
}