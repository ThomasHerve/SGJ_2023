using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
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

    [Tooltip("Mutations")]
    public Mutation[] mutations;
    private Mutation[] currentMutations = new Mutation[2];


    void SetSwitchMutation(int num, bool value)
    {
        switch (mutations[num].index)
        {
            case 0:
                confuse = value;
                mutations[num].effect.gameObject.SetActive(value);
                break;
            case 1:
                sleep = value;
                mutations[num].effect.gameObject.SetActive(value);
                break;
        }

    }

    public void RemoveMutation(int num)
    {
        int index = 0;
        for(int i = 0; i < mutations.Length; i++)
        {
            if(mutations[i].index == num)
            {
                index = num;
            }
        }
        SetSwitchMutation(index, false);
        currentMutations[num] = new Mutation { index = -1 }; 
    }

    // Effects
    public void AddMutation(int index)
    {
        if (currentMutations[0].index == -1 || currentMutations[1].index == -1)
        {
            int num = 0;
            if (currentMutations[0].index != -1)
                num = 1;
            currentMutations[num] = mutations[index];

            // Switch mutation
            SetSwitchMutation(index, true);
        }
    }

    // Mutations var
    public bool confuse = false;
    public bool sleep = false;



    // Start is called before the first frame update
    void Start()
    {
        characterControler = GetComponent<CharacterControler2D>();
        menuPlayer.SetActive(true);
        lifebar = menuPlayer.GetComponentInChildren<LifeBar>();
        lifebar.SetMaxHealth(100);

        // Mutation
        currentMutations[0] = new Mutation { index = -1 };
        currentMutations[1] = new Mutation { index = -1 };

        // DEBUG
        AddMutation(0);
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
