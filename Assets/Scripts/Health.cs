using UnityEngine;
using UnityEngine.UI;
using TMPro; // 꼭 필요

public class Health : MonoBehaviour
{
    [Header("체력 설정")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("체력바 UI (옵션)")]
    public Slider healthBar;
    public TextMeshProUGUI healthText; // 현재 체력 숫자만 표시

    [Header("타입 설정")]
    public bool isPlayer;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString(); // ✅ 현재 체력 숫자만 표시
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        // 죽음 처리
    }
}
