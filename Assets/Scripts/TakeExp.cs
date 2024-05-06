using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeExp : MonoBehaviour
{
    PlayerHealth player;
    DamageEnemy damageEnemy;
    public GameObject popup;
    public TMP_Text text;
    [SerializeField] private int maxExp = 1;
    int currentExp;
    int lv = 1;

    public ExpBar expBar;

    private void Start()
    {
        currentExp = 0;
        expBar.UpdateBar(currentExp, maxExp, lv);
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        damageEnemy = GetComponentInChildren<DamageEnemy>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExpUp(5);
        }
    }

    public void ExpUp(int exp)
    {

        currentExp += exp;
        if (currentExp >= maxExp) 
        {
            text.text = "Level up";
            Instantiate(popup, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            currentExp = 0;
            maxExp *= 2;
            lv += 1;
            player.maxHealth += UnityEngine.Random.Range(5, 10);
            player.currentHealth = player.maxHealth;
            damageEnemy.minDamageAttack1 += UnityEngine.Random.Range(1, 2);
            damageEnemy.maxDamageAttack1 += UnityEngine.Random.Range(1, 2);
            damageEnemy.minDamageAttack2 += UnityEngine.Random.Range(2, 3);
            damageEnemy.maxDamageAttack2 += UnityEngine.Random.Range(2, 3);
            damageEnemy.minDamageAttack3 += UnityEngine.Random.Range(3, 4);
            damageEnemy.maxDamageAttack3 += UnityEngine.Random.Range(3, 4);
        }
        expBar.UpdateBar(currentExp, maxExp, lv );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Exp")
        {
            Destroy(collision.gameObject);
            int random = UnityEngine.Random.Range(1, 5);
            ExpUp(random);
        }
    }
}
