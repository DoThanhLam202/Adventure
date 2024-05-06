using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private bool isMovingRight = true;
    private Rigidbody2D rb;
    private EnemyHealth health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (health.currentHealth <= 0)
            return;
        Move();
    }

    private void Move()
    {
        Vector2 mov = isMovingRight ? Vector2.right : Vector2.left;
        rb.velocity = mov * speed * Time.deltaTime;
        if (!isMovingRight)
        {
            transform.localScale = new Vector3(-5f,5f,5f);

        }
        else if (isMovingRight)
        {
            transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            return;
        }
        isMovingRight = !isMovingRight;
    }
}
