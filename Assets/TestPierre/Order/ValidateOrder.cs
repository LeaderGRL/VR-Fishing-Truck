using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ValidateOrder : MonoBehaviour
{

    [SerializeField] private OrderManager orderManager;
    [SerializeField] private Plate platePrefab;
    [SerializeField] private Transform spawnPosition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate")){
            Debug.Log("Plate attempt to validate");
            var plate = other.GetComponent<Plate>();
            if (plate.TypeOrder == OrderType.Fish || plate.TypeOrder == OrderType.CutFish || plate.TypeOrder == OrderType.Boot)
            {
                Debug.Log("VALIDATE ?");
                orderManager.ValidateOrder(plate.TypeOrder);
                Destroy(plate. interactable.transform.gameObject);
                Destroy(plate.gameObject);
            }
            else
            {
                Debug.Log("not valid");
                if(!plate.interactable.IsUnityNull()) Destroy(plate?.interactable.transform.gameObject);
                Destroy(plate.gameObject);
            }

            //Spawn another plate
            var spawnPlate = Instantiate(platePrefab);
            spawnPlate.transform.position = spawnPosition.position;
            spawnPlate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
