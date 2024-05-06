using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private float speed = 100f;
    [SerializeField] private float speedForce = 160f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private TrailRenderer tr;
    public bool canRoll = true;
    private bool isRolling;
    [SerializeField] private float RollPow = 10f;
    private float Rolling_time = 0.2f;
    private float RollCooldown = 5f;
    public bool isGround;
    public bool isWall;
    private Rigidbody2D rb;


    private float Speedter()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            return speedForce;
        }
        else
            return speed;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        isGround = true;
        isWall = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isRolling) return;
        OnMove();
        OnJump();
        onSkill();
    }

    private void OnMove()
    {
        if(isWall)
        {
            return;
        }
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * Speedter() * Time.fixedDeltaTime, rb.velocity.y);
        if(inputHorizontal < 0 )
        {
            transform.localScale = new Vector3(-6f, 6f, 6f);
        }
        else if(inputHorizontal > 0 )
        {
            transform.localScale = new Vector3(6f, 6f, 6f);
        }
    }
    private void OnJump()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        if (inputVertical > 0 && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGround = false;
        }
    }

    private void onSkill()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.P) && canRoll && inputHorizontal != 0)
        {
            StartCoroutine(Roll());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            isGround = false;
            isWall = true;
        }
        else
        {
            isGround = true;
            isWall = false;
        }
    }

    private IEnumerator Roll()
    {
        canRoll = false;
        isRolling = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0;
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float rollDirection = Mathf.Sign(inputHorizontal);
        rb.velocity = new Vector2(rollDirection * RollPow, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(Rolling_time);
        tr.emitting = false;
        rb.gravityScale = originGravity;
        isRolling = false;
        yield return new WaitForSeconds(RollCooldown);
        canRoll = true;
    }

    public bool canRolling()
    {
        if(isRolling)
        {
            return true;
        }
        else
            return false;
    }
}
