using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    private Collider spawnCollider;
    // Start is called before the first frame update
    [SerializeField] private GameObject[] objectPrefabsToSpawn;

    private float maxX;
    private float maxZ;
    private float minX;
    private float minZ;
    private float posY;
    void Start()
    {
        spawnCollider = GetComponent<Collider>();
        maxX = spawnCollider.bounds.max.x;
        maxZ = spawnCollider.bounds.max.z;
        minX = spawnCollider.bounds.min.x;
        minZ = spawnCollider.bounds.min.z;
        posY = spawnCollider.transform.position.y;
    }

    public void SpawnFish()
    {
        var obj = Instantiate(objectPrefabsToSpawn[Random.Range(0, objectPrefabsToSpawn.Length)]);
        obj.transform.position = new Vector3(Random.Range(minX, maxX), posY, Random.Range(minZ, maxZ));
    }
}
