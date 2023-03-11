using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler2D : MonoBehaviour
{
    private Rigidbody2D rigidbody;


    private Vector2 movementInput,targetInput,influence;
    public bool isdead;
    private SpriteRenderer spriteRendererBras;
    float cd = 0f;
  
    private bool shootInput;
    [SerializeField]
    private GameObject PivotBras,Canon,balle;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float high ;

    [SerializeField]
    AudioClip shootClip;


    public bool isKeyboard = false;
    private bool isRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRendererBras = PivotBras.GetComponentInChildren<SpriteRenderer>();
        isdead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(isdead);
        if (!isdead)
        {
            Vector2 vel;
            if (influence != Vector2.zero)
            {
                vel = influence * Time.deltaTime * speed;
                transform.Find("JetPack").GetComponent<SoundFade>().FadeOut();
            }
            else
            {
                vel = movementInput * Time.deltaTime * speed;
                if (movementInput == Vector2.zero)
                    transform.Find("JetPack").GetComponent<SoundFade>().FadeOut();
            }
            rigidbody.velocity = vel;

        if(transform.childCount > 1)
        {
            return;
        }
        // flip du sprite 
        if(!isKeyboard)
        {
            if (targetInput.x > 0)
            {
                if (targetInput.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); ;
                }
                else if (targetInput.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0); ;
                }
            }

            // Cas clavier
            if (isKeyboard)
            {
                if (movementInput.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    spriteRendererBras.gameObject.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                    PivotBras.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (movementInput.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -180);
                    spriteRendererBras.gameObject.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                    PivotBras.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
            else
            {
                //flip des bras
                PivotBras.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(targetInput, Vector2.right));
            }
            //fonction de shoot
            if (shootInput && Time.time - cd > 2)
            {
                cd = Time.time;
                //Tirer
                GameObject g = GameObject.Instantiate(balle, Canon.transform.position, Quaternion.Euler(0, 0, 0));
                ondes o = g.GetComponent<ondes>();
                o.direction = targetInput;
                o.shooter = this.gameObject;
            }
        }
        
        // Cas clavier
        if(isKeyboard)
        {
            if (movementInput.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                spriteRendererBras.gameObject.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                PivotBras.transform.rotation = Quaternion.Euler(0, 0, 0);
                isRight = true;
            }
            else if (movementInput.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -180);
                spriteRendererBras.gameObject.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                PivotBras.transform.rotation = Quaternion.Euler(0, 0, 180);
                isRight = false;
            }
        }
        else
        {
            //flip des bras
            PivotBras.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(targetInput, Vector2.right));
        }
        //fonction de shoot
        if (shootInput &&  Time.time -cd > 2)
        {
            cd = Time.time;
            //Tirer
            GameObject g =  GameObject.Instantiate(balle, Canon.transform.position,Quaternion.Euler(0,0,0));
            g.transform.SetParent(gameObject.transform);
            ondes o =g.GetComponent<ondes>();
            if(!isKeyboard)
            {
                o.direction = targetInput;
            }
            else
            {
                if(isRight)
                {
                    o.direction = Vector2.right;
                }
                else
                {
                    o.direction = Vector2.left;
                }
            }
            
            o.shooter = this.gameObject;
        }
  
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        transform.Find("JetPack").GetComponent<SoundFade>().FadeIn();
    }

    public void Target(InputAction.CallbackContext context)
    {
        targetInput = context.ReadValue<Vector2>();
    }


    public void shoot(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValueAsButton();
        GetComponent<AudioSource>().PlayOneShot(shootClip);
    }

    public void AddForce(Vector2 direction, float duration)
    {
        StartCoroutine(AddForceCoroutine(direction, duration));
    }

    IEnumerator AddForceCoroutine(Vector2 direction, float duration)
    {
        influence = direction;
        yield return new WaitForSeconds(duration);
        influence = Vector2.zero;
    }
}
