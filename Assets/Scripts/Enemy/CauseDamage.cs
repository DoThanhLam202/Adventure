using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    PlayerHealth player;
    EnemyHealth enemy;
    private float time_start = 0.5f;
    private int minDamage = 1;
    private int maxDamage = 4;
    private float time_repeat = 0.5f;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (enemy.currentHealth <= 0) return;
            player = collision.GetComponent<PlayerHealth>();
            InvokeRepeating("DamagePlayer", time_start, time_repeat);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
            CancelInvoke();
        }
    }

    private void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        player.TakeDame(damage);
    }
}
