using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int vie = 100;
    public int score = 0;
    public string color = "red";

    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private float knockbackDuration;

    private CharacterControler2D characterControler;
    public GameObject menuPlayer;
    private LifeBar lifebar;
    [SerializeField]
    private SpriteRenderer corps;
    [SerializeField]
    private SpriteRenderer bras;
    [SerializeField]
    private SpriteRenderer slowCorps;
    [SerializeField]
    private SpriteRenderer slowBras;
    
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

    internal void Reinit()
    {
        vie = 100;
        if(characterControler!=null)
        characterControler.isdead = false;
        if (lifebar != null)
            lifebar.SetHealth(vie);
    }

    public void TakeDamage(int damage)
    {
        vie -= damage;
        if (vie < 0) { vie = 0;}
        if (vie == 0) Die();
        lifebar.SetHealth(vie);
    }

    public void TakeDamage(int damage,GameObject shooter)
    {
        vie -= damage;
        if (vie < 0) { vie = 0; }
        if (vie == 0) Die();
        lifebar.SetHealth(vie);
    }

    private void Die()
    {
        characterControler.isdead = true;
    }

    internal void changeSkin(Sprite sprite1, Sprite sprite2, Sprite sprite3, Sprite sprite, string v)
    {
        bras.sprite = sprite2;
        corps.sprite = sprite1;
        //slowBras.sprite = sprite;
        color = v;
    }
}
