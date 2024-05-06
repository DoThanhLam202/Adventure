using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private bool isAttacking;
    private float time_start_attack1 = 0.5f;
    private float time_start_attack2 = 0.7f;
    private float time_start_attack3 = 0.5f;

    public int minDamageAttack1 = 2;
    public int maxDamageAttack1 = 5;
    public int minDamageAttack2 = 3;
    public int maxDamageAttack2 = 7;
    public int minDamageAttack3 = 8;
    public int maxDamageAttack3 = 10;

    private void Start()
    {
        isAttacking = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && isAttacking)
        {
            StartCoroutine(Attack(time_start_attack1));
        }
        else if (Input.GetKeyDown(KeyCode.K) && isAttacking)
        {
            StartCoroutine(Attack(time_start_attack2));
        }
        else if (Input.GetKeyDown(KeyCode.L) && isAttacking)
        {
            StartCoroutine(Attack(time_start_attack3));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isAttacking = false;
        }
    }

    IEnumerator Attack(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemy = col.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    int damage = 0;
                    if (delay == time_start_attack1)
                        damage = Random.Range(minDamageAttack1, maxDamageAttack1);
                    else if (delay == time_start_attack2)
                        damage = Random.Range(minDamageAttack2, maxDamageAttack2);
                    else if (delay == time_start_attack3)
                        damage = Random.Range(minDamageAttack3, maxDamageAttack3);

                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
