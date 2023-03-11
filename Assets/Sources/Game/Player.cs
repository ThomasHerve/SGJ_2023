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

    // Start is called before the first frame update
    void Start()
    {
        characterControler = GetComponent<CharacterControler2D>();
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
        if (vie < 0) vie = 0;
    }
}
