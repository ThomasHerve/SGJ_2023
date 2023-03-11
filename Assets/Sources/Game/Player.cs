using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int vie = 100;
    public int score = 0;
    public string color = "white";

    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private float knockbackDuration;

    private CharacterControler2D characterControler;
    public GameObject menuPlayer;
    private LifeBar lifebar;

    // Start is called before the first frame update
    void Start()
    {
        characterControler = GetComponent<CharacterControler2D>();
        menuPlayer.SetActive(true);
        lifebar = menuPlayer.GetComponentInChildren<LifeBar>();
        lifebar.SetMaxHealth(100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Knockback(Vector2 other)
    {
        characterControler.AddForce((new Vector2(transform.position.x, transform.position.y) - other).normalized * knockbackForce, knockbackDuration);
        //rb.AddForce((new Vector2(transform.position.x, transform.position.y) - other).normalized * knockbackForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        vie -= damage;
        if (vie < 0) { vie = 0;}
        if (vie == 0) Die();
        lifebar.SetHealth(vie);
    }

    private void Die()
    {
        characterControler.isdead = true;
    }
}
