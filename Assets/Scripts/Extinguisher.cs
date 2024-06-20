using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private float amountExtinguishedPerSecond = 1f;
    [SerializeField] private WaterJet waterJet;
    [SerializeField] private AudioSource SFX_Water;
    [SerializeField] private Transform raycastStartPoint;

    void Start()
    {
        
    }

    void Update()
    {

        //Fire();

        RaycastHit hit;
        Debug.DrawRay(raycastStartPoint.position, raycastStartPoint.forward * 10f, Color.red);
        if (Physics.Raycast(raycastStartPoint.position, raycastStartPoint.forward, out hit, 10f))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent(out VolumetricFire fire))
            {
                Debug.Log("Extinguishing !");
                fire.thickness -= 2 * Time.deltaTime;
            }
        }
    }

    public void Fire()
    {
        SFX_Water.volume = 0.5f;
        waterJet.Emit();
    }

    public void StopFire() {
        waterJet.StopEmit();
        SFX_Water.volume = 0f;
    }
}
