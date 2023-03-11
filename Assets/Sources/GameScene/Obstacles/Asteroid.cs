using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Obstacle
{

    [SerializeField]
    private float rotateSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        GetComponent<Rigidbody2D>().rotation += rotateSpeed * Time.deltaTime;
    }

}
