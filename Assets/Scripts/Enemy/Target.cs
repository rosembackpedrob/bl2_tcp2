using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Target : MonoBehaviour
{
    [Header("Target Health")]
    [SerializeField] private float health = 50f;
    [SerializeField] private float lerpTimer;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float chipSpeed = 2f;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject moneyModel;
    [SerializeField] private float money = 0f;
    [SerializeField] private TextMeshProUGUI moneyText;

    public void TakeDamage(float amount)
    {
        FindObjectOfType<AudioManager>().Play("BullymongHit");
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

        UpdateHealthUI();
    }

    void Die()
    {
        Destroy(gameObject, 0.5f);
        DropCoin();

    }

    void DropCoin()
    {
        Vector3 position = transform.position; // position of the enemy
        GameObject coin = Instantiate(moneyModel, position,Quaternion.identity); // Coin Drop

        moneyText.text = "Money:" + Mathf.Round(money);
        Destroy(coin, 1f);
        money += 2;
    }

    void UpdateHealthUI()
    {
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

}
