using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private CuttedFish cuttedFish;

    public int knifeCutCount = 0;

    public void IncrementCut()
    {
        knifeCutCount++;
        if (knifeCutCount >= 3)
        {
            spawnCuttedFish();
        }
    }

    private void spawnCuttedFish()
    {
        var cutfish = Instantiate(cuttedFish);
        cutfish.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}