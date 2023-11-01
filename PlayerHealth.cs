using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isHurt = false;
    public Text healthText;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        isHurt = true;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("HomeScrene");
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            StartCoroutine(ContinuousDamage());
        }
        if (other.CompareTag("Demon"))
        {
            StartCoroutine(ContinuousDamage2());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            StopCoroutine(ContinuousDamage());
            isHurt = false;
        }
        if (other.CompareTag("Demon"))
        {
            StopCoroutine(ContinuousDamage2());
            isHurt = false;
        }
    }

    // Coroutine for continuous damage from fire
    private IEnumerator ContinuousDamage()
    {
        while (true)
        {
            CameraShake.Shake(0.2f, 0.2f);
            TakeDamage(10f); // Adjust the damage rate as needed
            yield return new WaitForSeconds(1f); // Adjust the frequency of damage
            if (isHurt==false)
            {
                break;
            }
        }
    }
    private IEnumerator ContinuousDamage2()
    {
        while (true)
        {
            CameraShake.Shake(0.4f, 0.4f);
            TakeDamage(40f); // Adjust the damage rate as needed
            yield return new WaitForSeconds(1f); // Adjust the frequency of damage
            if (isHurt == false)
            {
                break;
            }
        }
    }
}

