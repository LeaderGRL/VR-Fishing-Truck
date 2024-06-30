using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Plate : MonoBehaviour
{
    //private bool isComplete = false;
    //public bool IsComplete => isComplete;

    public OrderType TypeOrder { get; private set; }
    public IXRInteractable interactable { get; private set; }

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }
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

    public void HoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Hover Enter");
        body.isKinematic = true;
        body.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void HoverExit(HoverExitEventArgs args)
    {
        Debug.Log("Hover Exit");
        body.isKinematic = false;
        body.constraints = RigidbodyConstraints.None;
    }

    public void SnapFish(SelectEnterEventArgs args)
    {
        interactable = args.interactableObject;
        if (interactable.transform.CompareTag("CutFish"))
        {
            Debug.Log("SNAP CUT FISH");
            var fish = interactable.transform.GetComponent<CuttedFish>();
            if(fish.IsCooked)
            {
                Debug.Log("FISH IS COOKED");
                //isComplete = true;
                TypeOrder = OrderType.CookedFish;
            }
            else
            {
                Debug.Log("FISH IS NOT COOKED");
                TypeOrder = OrderType.RawFish;
                //isComplete = false;
            }
        }
        else if(interactable.transform.CompareTag("Fish"))
        {
            //isComplete = false;
            Debug.Log("Object is Fish...");
            TypeOrder = OrderType.None;
        }
        else if (interactable.transform.CompareTag("Boot"))
        {
            //isComplete = false;
            Debug.Log("Object is Boot...");
            TypeOrder = OrderType.Boot;
        }
        interactable.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        body.isKinematic = false;
    }
    public void UnSnapFish(SelectExitEventArgs args)
    {
        args.interactableObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("UNSNAP INGREDIENT");
        //isComplete = false;
        interactable = null;
        body.isKinematic = true;
        TypeOrder = OrderType.None;
    }
}
