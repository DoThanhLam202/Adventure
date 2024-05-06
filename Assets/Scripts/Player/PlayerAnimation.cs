using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private GameObject enemy;
    private float heal = 100;

    private bool canAttack1 = true;
    private bool canAttack2 = true;
    private bool canAttack3 = true;

    private float attack1Cooldown = 0.5f;
    private float attack2Cooldown = 0.7f;
    private float attack3Cooldown = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemy = GameObject.Find("Enemy");
    }

    private void Update()
    {
        onMove();
        onAttack();
        onSkill();
        onHurt();
    }

    private void onHurt()
    {
        if(playerHealth.currentHealth < heal)
        {
            if (IsEnemyLeft() && IsPlayerLeft())
            {
                animator.SetTrigger(StringAnimation.HitFront);
            }
            else if (IsEnemyLeft() && !IsPlayerLeft())
            {
                animator.SetTrigger(StringAnimation.HitBack);
            }
            else if (!IsEnemyLeft() && IsPlayerLeft())
            {
                animator.SetTrigger(StringAnimation.HitBack);
            }
            else if (!IsEnemyLeft() && !IsPlayerLeft())
            {
                animator.SetTrigger(StringAnimation.HitFront);
            }

            heal = playerHealth.currentHealth;
        }
    }

    private bool IsPlayerLeft()
    {
        return enemy.transform.position.x < transform.position.x;
    }

    private bool IsEnemyLeft()
    {
        Vector3 playerScale = transform.localScale;
        return playerScale.x < 0;
    }

    private void onMove()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (inputHorizontal != 0)
        {
            animator.SetBool(StringAnimation.Walk, true);
            animator.SetBool(StringAnimation.Run, false);
        }
        else
        {
            animator.SetBool(StringAnimation.Walk, false);
        }
        if (Input.GetKey(KeyCode.LeftShift) && inputHorizontal != 0)
        {
            animator.SetBool(StringAnimation.Run, true);
            animator.SetBool(StringAnimation.Walk, false);
        }
        else
        {
            animator.SetBool(StringAnimation.Run, false);
        }


        if (rb.velocity.y > 0.01)
        {
            animator.SetBool(StringAnimation.Jump, true);
            animator.SetBool(StringAnimation.Fall, false);
        }
        else if(rb.velocity.y < -0.01)
        {
            animator.SetBool(StringAnimation.Jump, false);
            animator.SetBool(StringAnimation.Fall, true);
        }
        else if(playerController.isGround)
        {
            animator.SetBool(StringAnimation.Jump, false);
            animator.SetBool(StringAnimation.Fall, false);
        }
    }

    private void onAttack()
    {
        if(Input.GetKeyDown(KeyCode.J) && canAttack1)
        {
            animator.SetTrigger(StringAnimation.Attack1);
            StartCoroutine(AttackCooldown(attack1Cooldown, 1));
        }
        if (Input.GetKeyDown(KeyCode.K) && canAttack2)
        {
            animator.SetTrigger(StringAnimation.Attack2);
            StartCoroutine(AttackCooldown(attack2Cooldown, 2));
        }
        if (Input.GetKeyDown(KeyCode.L) && canAttack3)
        {
            animator.SetTrigger(StringAnimation.Attack3);
            StartCoroutine(AttackCooldown(attack3Cooldown, 3));
        }
    }

    private IEnumerator AttackCooldown(float cooldownTime, int attackIndex)
    {
        switch (attackIndex)
        {
            case 1:
                canAttack1 = false;
                break;
            case 2:
                canAttack2 = false;
                break;
            case 3:
                canAttack3 = false;
                break;
        }

        yield return new WaitForSeconds(cooldownTime);

        switch (attackIndex)
        {
            case 1:
                canAttack1 = true;
                break;
            case 2:
                canAttack2 = true;
                break;
            case 3:
                canAttack3 = true;
                break;
        }
    }

    private void onSkill()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.P) && inputVertical == 0)
        {
            animator.SetTrigger(StringAnimation.Roll);
        }
    }
}
