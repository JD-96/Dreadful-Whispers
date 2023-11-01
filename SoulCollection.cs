using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoulCollection : MonoBehaviour
{
    private List<string> collectedSouls = new List<string>();
    public Dictionary<string, Sprite> soulImages;
    public List<Image> collectedSoulImages; // Assign the UI Image components in the Inspector to display the collected souls.
    [SerializeField] private Sprite blueSoulSprite;
    [SerializeField] private Sprite greenSoulSprite;
    [SerializeField] private Sprite orangeSoulSprite;
    [SerializeField] private Sprite redSoulSprite;
    [SerializeField] private Sprite yellowSoulSprite;
    [SerializeField] private GameObject spawnableObject;
    [SerializeField] private GameObject spawnableObject2;

    private bool allSoulsCollected = false; // Track whether all 5 souls are collected

    private void Start()
    {
        soulImages = new Dictionary<string, Sprite>
        {
            { "BlueSoul", blueSoulSprite },
            { "GreenSoul", greenSoulSprite },
            { "OrangeSoul", orangeSoulSprite },
            { "RedSoul", redSoulSprite },
            { "YellowSoul", yellowSoulSprite }
        };
        foreach (var image in collectedSoulImages)
        {
            image.gameObject.SetActive(false);
        }
        allSoulsCollected = false;
        ParticleSystem particleSystem = spawnableObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Stop(); // This stops the particle emission.
        }
        ParticleSystem particleSystem2 = spawnableObject2.GetComponent<ParticleSystem>();
        particleSystem2.Stop(); 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collectedSoul = other.gameObject;
        string soulTag = collectedSoul.tag; 

        // Check if it's a soul, if it hasn't been collected already, and if it has a sprite image.
        if (soulImages.ContainsKey(soulTag) && !collectedSouls.Contains(soulTag))
        {
            // Add the collected soul to the list
            collectedSouls.Add(soulTag);

            // Display the collected soul as an image
            DisplayCollectedSoul(soulTag);

            // Destroy the collected soul GameObject
            Destroy(collectedSoul);

            if (collectedSouls.Count == 5 && !allSoulsCollected)
            {
                // Set the flag to true and spawn the object at the specified location
                allSoulsCollected = true;
                SpawnObject();
                Debug.Log("Souls collected");
            }
        }
        if (other.CompareTag("finish") && allSoulsCollected)
        {
            ParticleSystem particleSystem2 = spawnableObject2.GetComponent<ParticleSystem>();

            particleSystem2.Play();
            Destroy(spawnableObject);

            Invoke("EndGame", 7.0f);

        }
    }

    private void DisplayCollectedSoul(string soulTag)
    {
        // Use the collectedSoulImages to display the collected souls as images from left to right.
        for (int i = 0; i < collectedSoulImages.Count; i++)
        {
            if (i < collectedSouls.Count && soulImages.ContainsKey(collectedSouls[i]))
            {
                collectedSoulImages[i].sprite = soulImages[collectedSouls[i]];
                collectedSoulImages[i].gameObject.SetActive(true);
            }
            else
            {
                collectedSoulImages[i].gameObject.SetActive(false);
            }
        }
    }

    private void SpawnObject()
    {
        ParticleSystem particleSystem = spawnableObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play(); // This starts the particle emission.
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}
