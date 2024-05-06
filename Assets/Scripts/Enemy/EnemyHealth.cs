using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int currentHealth;
    public GameObject popup;
    public TMP_Text text;
    public UnityEvent onEvent;
    public GameObject exp;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        text.text = damage.ToString();
        Instantiate(popup, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onEvent.Invoke();
            Instantiate(exp, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}