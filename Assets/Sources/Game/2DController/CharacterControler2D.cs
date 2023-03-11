using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler2D : MonoBehaviour
{
    private Rigidbody2D rigidbody;


    private Vector2 movementInput;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float high ;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(movementInput);
        rigidbody.velocity = movementInput * Time.deltaTime * speed;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
