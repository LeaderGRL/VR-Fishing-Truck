using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutBoard : MonoBehaviour
{

    private bool fishIsSnapped;
    public bool FishIsSnapped => fishIsSnapped;
    public Fish currentFish;
    

    public void SnapFish(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Fish"))
        {
            Debug.Log("SNAP FISH");
            fishIsSnapped = true;
            currentFish = args.interactableObject.transform.GetComponent<Fish>();
        }
        else
        {
            fishIsSnapped = false;
            Debug.Log("Object is not GrabInteractable...");
            currentFish = null;
        }
    }
    public void UnSnapFish(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("Fish"))
        {
            Debug.Log("UNSNAP FISH");
        }
        fishIsSnapped = false;
        currentFish = null;
    }
}