using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnemyAI : MonoBehaviour
{
    private Animator animator;
    private EnemyHealth health;

    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        health = GameObject.Find("Enemy(Clone)").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        onDeath();
    }

    private void onDeath()
    {
        if(health.currentHealth <= 0)
        {
            animator.SetTrigger(StringAnimation.EvilTreeDeath);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(health.currentHealth <= 0) return;
            Animator enemyAnimator = transform.parent.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool(StringAnimation.EvilTreeAttack, true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Animator enemyAnimator = transform.parent.GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool(StringAnimation.EvilTreeAttack, false);
        }
    }
}
