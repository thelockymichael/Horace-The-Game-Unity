using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ItemManifest : MonoBehaviour
{

    public GameObject[] items;
    public bool[] itemsCollected;

    // Use this for initialization
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Manifest").Length < 1)
        {
            DontDestroyOnLoad(this);
            //PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        }
        this.tag = "Manifest";
        if (PlayerPrefs.GetString("LastScene") == SceneManager.GetActiveScene().name && GameObject.FindGameObjectsWithTag("Manifest").Length > 1)
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

        itemsCollected = new bool[items.Length];
    }

    void Update()
    {
        if (PlayerPrefs.GetString("LastScene") != SceneManager.GetActiveScene().name)
        {
            Destroy(gameObject);
        }
    }

    public void setCollected(int i)
    {
        itemsCollected[i] = true;
    }
}
     
 