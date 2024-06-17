using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{
    [SerializeField]
    private GameObject _smokeEffect;

    private IHeatable _heatable;

    // Start is called before the first frame update
    void Start()
    {
        _smokeEffect.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_heatable != null)
        {
            _heatable.Heat();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HotPlate is heating up");

        // check if collision object has IHeatable interface
        _heatable = collision.gameObject.GetComponent<IHeatable>();

        if (_heatable != null)
        {
            Debug.Log("HotPlate detected a heatable object: " + collision.gameObject.name);
            _smokeEffect.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            Debug.Log("Collision object does not have IHeatable interface: " + collision.gameObject.name);
            _smokeEffect.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("HotPlate is cooling down");

        // Check if the exiting object is the same as the current heatable object
        if (collision.gameObject.GetComponent<IHeatable>() == _heatable)
        {
            _heatable = null;
            _smokeEffect.GetComponent<ParticleSystem>().Stop();
        }
    }
}