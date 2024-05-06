using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    private Animator enemyAnimator;
    private void Start()
    {
        enemyAnimator = transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           // if (Input.GetKeyDown(KeyCode.J))
           // {
                enemyAnimator.SetTrigger(StringAnimation.EvilTreeHurt);
           // }
        }
    }
}
