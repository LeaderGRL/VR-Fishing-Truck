using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlate : MonoBehaviour
{

    [SerializeField] private GameObject _smokeEffect;
    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private AudioSource _cookingAudioSource;
    [SerializeField] private AudioSource _timerAudioSource;

    private Coroutine heatingCoroutine;
    private Coroutine timerAudioCoroutine;
    private IHeatable _heatable;

    public int _timeToEnflame = 10;


    // Start is called before the first frame update
    void Start()
    {
        _smokeEffect.GetComponent<ParticleSystem>().Stop();
        _fireEffect.SetActive(false);
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

            if (_timerAudioSource)
            {
                Debug.Log("Playing timer audio source");
                //_timerAudioSource.enabled = true;
                _timerAudioSource.Play();
                timerAudioCoroutine = StartCoroutine(StopTimerAudioSource());
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if collision object has IHeatable interface
        _heatable = collision.gameObject.GetComponent<IHeatable>();

        if (_heatable != null)
        {
            _smokeEffect.GetComponent<ParticleSystem>().Play();

            if (_cookingAudioSource)
            {
                //_cookingAudioSource.enabled = true;
                _cookingAudioSource.Play();
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

            if (_cookingAudioSource)
            {
                //_cookingAudioSource.enabled = false;
                _cookingAudioSource.Stop();
            }

            if (heatingCoroutine != null)
            {
                StopCoroutine(heatingCoroutine);
                heatingCoroutine = null;
            }

            if (timerAudioCoroutine != null)
            {
                StopCoroutine(timerAudioCoroutine);
                timerAudioCoroutine = null;
            }
        }
    }

    private IEnumerator Enflame()
    {
        yield return new WaitForSeconds(_timeToEnflame);
        Debug.Log("HotPlate is enflaming");

        _fireEffect.SetActive(true);
    }

    private IEnumerator StopTimerAudioSource()
    {
        yield return new WaitForSeconds(_timeToEnflame);
        //_timerAudioSource.enabled = false;
        _timerAudioSource.Stop();
    }
}