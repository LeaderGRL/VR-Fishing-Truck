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
        if (args.interactableObject.transform.CompareTag("CutFish"))
        {
            Debug.Log("SNAP CUT FISH");
            interactable = args.interactableObject;
            var fish = args.interactableObject.transform.GetComponent<CuttedFish>();
            if(fish.IsCooked)
            {
                Debug.Log("FISH IS COOKED");
                //isComplete = true;
                TypeOrder = OrderType.CutFish;
            }
            else
            {
                Debug.Log("FISH IS NOT COOKED");
                //isComplete = false;
            }
        }
        else if(args.interactableObject.transform.CompareTag("Fish"))
        {
            //isComplete = false;
            Debug.Log("Object is not FISH...");
            TypeOrder = OrderType.Fish;
        }
        else if (args.interactableObject.transform.CompareTag("Boot"))
        {
            //isComplete = false;
            Debug.Log("Object is not FISH...");
            TypeOrder = OrderType.Boot;
        }
        args.interactableObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        body.isKinematic = false;
    }
    public void UnSnapFish(SelectExitEventArgs args)
    {
        args.interactableObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if (args.interactableObject.transform.CompareTag("CutFish"))
        {
            Debug.Log("UNSNAP FISH");
        }
        //isComplete = false;
        interactable = null;
        body.isKinematic = true;
        TypeOrder = OrderType.None;
    }
}
