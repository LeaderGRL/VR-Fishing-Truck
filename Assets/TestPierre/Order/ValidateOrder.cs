using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateOrder : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] particleSystems;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate")){
            Debug.Log("Plate is trying to validate");
            var plate = other.GetComponent<Plate>();
            if (plate.IsComplete)
            {
                Debug.Log("VALIDAAAAAAATE");
                Destroy(plate.gameObject);
                foreach (var particle in particleSystems)
                {
                    particle.Play();
                }
            }
            else
            {
                Debug.Log("not valid");
            }
        }
    }
}
