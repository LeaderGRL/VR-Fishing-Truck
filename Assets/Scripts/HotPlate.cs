using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{
    [SerializeField]
    private GameObject _smokeEffect;
    [SerializeField]
    private GameObject _fireEffect;
    
    private Coroutine heatingCoroutine;
    private IHeatable _heatable;

    // Start is called before the first frame update
    void Start()
    {
        _smokeEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.SetActive(false);
        //foreach (ParticleSystem particleSystem in _fireEffect.GetComponentsInChildren<ParticleSystem>())
        //{
        //    particleSystem.Stop();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (_heatable == null)
        {
            return;
        }

        _heatable.Heat();

        if (_heatable.isCooked && heatingCoroutine == null)
        {
            heatingCoroutine = StartCoroutine(Enflame());
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

            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().enabled = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            Debug.Log("Collision object does not have IHeatable interface: " + collision.gameObject.name);
            _smokeEffect.GetComponent<ParticleSystem>().Stop();
            //_fireEffect.SetActive(false);
            //foreach (ParticleSystem particleSystem in _fireEffect.GetComponentsInChildren<ParticleSystem>())
            //{
            //    particleSystem.Stop();
            //}
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
            //_fireEffect.SetActive(false);
            //foreach (ParticleSystem particleSystem in _fireEffect.GetComponentsInChildren<ParticleSystem>())
            //{
            //    particleSystem.Stop();
            //}
        }
    }

    private IEnumerator Enflame() {
        yield return new WaitForSeconds(5);
        Debug.Log("HotPlate is enflaming");
        _fireEffect.SetActive(true);
        //foreach (ParticleSystem particleSystem in _fireEffect.GetComponentsInChildren<ParticleSystem>())
        //{
        //    particleSystem.Play();
        //}
    }
}