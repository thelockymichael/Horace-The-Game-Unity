using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
  
    public AudioSource sound;
    public ItemManifest manifest;

    private bool collected;
    // Use this for initialization
    void Awake()
    {
      
        collected = false;

        manifest = GameObject.Find("ItemManifest").GetComponent<ItemManifest>();
    }

    // Update is called once per frame
    void Update()
    {
      //  if (!sound.isPlaying && collected == true)
        //    Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && collected == false)
        {
            collected = true; //used to determine when to Destroy the gameObject

            //search through item array to find this item
            for (int i = 0; i < manifest.items.Length; i++)
            {
                if (this.transform.parent.gameObject == manifest.items[i])
                {
                    manifest.setCollected(i);
                }
            }
            //if-else block to determine effect of item
            if (this.gameObject.tag == "Coin")
            {
               // GameManager.Instance.currency += 1;

            }/*
            else if (this.gameObject.tag == "Health")
            {
                player.hearts = 3;
            }
            else if (this.gameObject.tag == "Life")
            {
                player.lives += 1;
            }*/
            //play coin/health/life pickup sound
            sound.Play();
            //remove game object from view, later destroyed when sound is not playing via update function
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}