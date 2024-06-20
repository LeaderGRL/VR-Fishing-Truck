using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    private AudioSource _audioSource;
    public GameObject audioPrefab;
    public AudioClip clip;

    // Start is called before the first frame update
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    void Start()
    {
        //SetVisual();
    }
    
    [ContextMenu("Consume")]
    public void Consume()
    {
        Destroy(Instantiate(audioPrefab),2);
        audioPrefab.GetComponent<AudioSource>().clip = clip;
        audioPrefab.GetComponent<AudioSource>().Play();
        
        Destroy(gameObject);
    }
}
