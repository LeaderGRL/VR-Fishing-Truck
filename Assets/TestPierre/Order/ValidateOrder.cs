using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValidateOrder : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private Plate platePrefab;
    [SerializeField] private Transform spawnPosition;

    private AudioSource audioSuccess;
    private void Start()
    {
        audioSuccess = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate")){
            Debug.Log("Plate attempt to validate");
            var plate = other.GetComponent<Plate>();
            if (plate.IsComplete)
            {
                Debug.Log("VALIDATE !");
                Destroy(plate.interactable.transform.gameObject);
                Destroy(plate.gameObject);
                foreach (var particle in particleSystems)
                {
                    particle.Play();
                }
                audioSuccess.Play();
                orderManager.ValidateOrder();
                var spawnPlate = Instantiate(platePrefab);
                spawnPlate.transform.position = spawnPosition.position;
                spawnPlate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Debug.Log("not valid");
                if(!plate.interactable.IsUnityNull()) Destroy(plate?.interactable.transform.gameObject);
                Destroy(plate.gameObject);
            }
        }
    }
}
