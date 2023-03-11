using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler2D : MonoBehaviour
{
    private Rigidbody2D rigidbody;


    private Vector2 movementInput,targetInput;
    private SpriteRenderer spriteRendererBras;
    float cd = 0f;
  
    private bool shootInput;
    [SerializeField]
    private GameObject PivotBras,Canon,balle;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float high ;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRendererBras = PivotBras.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = movementInput * Time.deltaTime * speed;

        // flip du sprite 
        if (targetInput.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); ;
        }
        else if (targetInput.x < 0)
        {
            transform.rotation = Quaternion.Euler( 0,180, 0);
            spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(180,0, 0); ;
        }
        
        //flip des bras
        PivotBras.transform.rotation=Quaternion.Euler(0, 0, -Vector2.SignedAngle(targetInput, Vector2.right));
        //fonction de shoot
        if (shootInput &&  Time.time -cd > 2)
        {
            cd = Time.time;
            //Tirer
            GameObject g=  GameObject.Instantiate(balle, Canon.transform.position,Quaternion.Euler(0,0,0));
            g.GetComponent<ondes>().direction= targetInput;
        }
  
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Target(InputAction.CallbackContext context)
    {
        targetInput = context.ReadValue<Vector2>();
    }


    public void shoot(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValueAsButton();
    }
}
