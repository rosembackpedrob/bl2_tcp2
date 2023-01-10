using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private float health;
    [SerializeField] private float lerpTimer;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float chipSpeed = 2f;

    [Header("Death and Respawn")]
    public int Respawn;
    public string RespawnString;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Damage Effect")]
    [SerializeField] private Image overlay;
    [SerializeField] private Image overlay2;
    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;

    private float durationTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color (overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        
        if(health < maxHealth / 3)
            return;
        FindObjectOfType<AudioManager>().Play("PlayerDamage");
        if(overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                //fade image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color (overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

    }

    public void UpdateHealthUI()
    {
        //Debug.Log("Health: " + health);

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth; //keeps the value between 0 and 1

        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount , percentComplete);
        }
        
        healthText.text = "" + Mathf.Round(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;

        durationTimer = 0;
        overlay.color = new Color (overlay.color.r, overlay.color.g, overlay.color.b, 1);
        if(health <= 0)
        {
            Die();
        }
        
    }

    public void Die()
    {
        SceneManager.LoadScene(Respawn);

        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    public void RestoreHealth (float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void ChangeTypeofDamage()
    {
        overlay = overlay2;
    }
}
