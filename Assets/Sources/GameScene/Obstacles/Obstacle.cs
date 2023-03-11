using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        rb.position += direction.normalized * speed * Time.deltaTime;
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 20)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Knockback(new Vector2(transform.position.x, transform.position.y));
        }
    }

    public void setup(Vector2 direction)
    {
        this.direction = direction;
    }

    
}
