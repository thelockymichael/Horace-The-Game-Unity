using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject coinPickup, healthPickup, lifePickup;
    private Object clone;
    public ItemManifest manifest;

    void Awake()
    {
        manifest = GetComponentInParent<ItemManifest>();
        StartCoroutine(startSpawn());

    }


    //IEnumerator to delay spawning items so ItemManifest has time to set collected array size.
    IEnumerator startSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        //spawn items if not collected
        for (int i = 0; i < manifest.items.Length; i++)
        {
            if (manifest.itemsCollected[i] == false)
            {
                Spawn(manifest.items[i]);
            }
        }
    }

    void Spawn(GameObject item)
    {

        if (item.tag == "CoinSpawn")
        {
            clone = Instantiate(coinPickup, item.transform.position, Quaternion.identity, item.transform);
            if (item.transform.childCount > 1)
            {
                Destroy(clone);
            }
        }
        else if (item.tag == "HealthSpawn")
        {
            clone = Instantiate(healthPickup, item.transform.position, Quaternion.identity, item.transform);
            if (item.transform.childCount > 1)
            {
                Destroy(clone);
            }
        }
        else if (item.tag == "LifeSpawn")
        {
            clone = Instantiate(lifePickup, item.transform.position, Quaternion.identity, item.transform);
            if (item.transform.childCount > 1)
            {
                Destroy(clone);
            }
        }
    }
}