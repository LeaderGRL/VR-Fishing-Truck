using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Plate : MonoBehaviour
{
    private bool isComplete = false;
    public bool IsComplete => isComplete;
    //[SerializeField] private ParticleSystem[] particleSystems;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Validate"))
    //    {
    //        if(isComplete)
    //        {
    //            foreach(var particle in particleSystems)
    //            {
    //                particle.Play();
    //            }
    //        }
    //        Destroy(gameObject);
    //    }
    //}

    public void SnapFish(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("CutFish"))
        {
            Debug.Log("SNAP CUT FISH");
            var fish = args.interactableObject.transform.GetComponent<CuttedFish>();
            if(fish.IsCooked)
            {
                Debug.Log("FISH IS COOKED");
                isComplete = true;
            }
            else
            {
                Debug.Log("FISH IS NOT COOKED");
                isComplete = false;
            }
        }
        else
        {
            isComplete = false;
            Debug.Log("Object is not GrabInteractable...");
        }
    }
    public void UnSnapFish(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("CuttedFish"))
        {
            Debug.Log("UNSNAP FISH");
        }
        isComplete = false;
    }
}
