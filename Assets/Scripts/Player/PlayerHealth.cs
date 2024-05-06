using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject popup;
    public TMP_Text text;
    public UnityEvent onEvent;

    public void OnEnable()
    {
        onEvent.AddListener(Destroy);
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void TakeDame(int damage)
    {
        currentHealth -= damage;
        text.text = damage.ToString();
        Instantiate(popup, new Vector3(transform.position.x, transform.position.y +1, transform.position.z), Quaternion.identity);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onEvent.Invoke();
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }
}
