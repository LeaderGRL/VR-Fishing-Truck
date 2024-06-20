using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{

    [SerializeField] private GameObject _smokeEffect;
    [SerializeField] private GameObject _fireEffect;
    
    private Coroutine heatingCoroutine;
    private IHeatable _heatable;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _smokeEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.SetActive(false);
        TryGetComponent<AudioSource>(out _audioSource);

    }

    // Update is called once per frame
    void Update()
    {
        if (_heatable == null)
        {
            return;
        }

        if (_heatable.IsCooked && heatingCoroutine == null)
        {
            heatingCoroutine = StartCoroutine(Enflame());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if collision object has IHeatable interface
        _heatable = collision.gameObject.GetComponent<IHeatable>();

        if (_heatable != null)
        {
            _smokeEffect.GetComponent<ParticleSystem>().Play();

            if (_audioSource)
            {
                _audioSource.enabled = true;
                _audioSource.Play();
            }

            _heatable.Heat();
        }
        else
        {
            _smokeEffect.GetComponent<ParticleSystem>().Stop();
        }

        
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the exiting object is the same as the current heatable object
        if (collision.gameObject.GetComponent<IHeatable>() == _heatable)
        {
            _heatable = null;
            _smokeEffect.GetComponent<ParticleSystem>().Stop();

            if (_audioSource)
            {
                _audioSource.enabled = false;
                _audioSource.Stop();
            }

            if (heatingCoroutine != null)
            {
                StopCoroutine(heatingCoroutine);
                heatingCoroutine = null;
            }
        }
    }

    private IEnumerator Enflame() {
        yield return new WaitForSeconds(5);
        Debug.Log("HotPlate is enflaming");

        _fireEffect.SetActive(true);
    }
}