using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler2D : MonoBehaviour
{
    private Rigidbody2D rigidbody;


    private Vector2 movementInput, targetInput, influence;
    public bool isdead;
    private SpriteRenderer spriteRendererBras;
    float cd = 0f;

    private bool shootInput;
    [SerializeField]
    private GameObject PivotBras, Canon, balle;
    [SerializeField]
    private float speed;
    [SerializeReference]
    private float shootCD = 2;
    [SerializeField]
    private float high;

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

            if (transform.Find("projectile(Clone)") != null)
            {
                return;
            }
            // flip du sprite 
            if (!isKeyboard)
            {
                if (targetInput.x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0); 
                    spriteRendererBras.gameObject.transform.localPosition = new Vector3(4.24f, 0.8f, 0);
                }
                else if (targetInput.x < 0 )
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0); ;
                    spriteRendererBras.gameObject.transform.localPosition = new Vector3(4.34f, -1.24f, 0);
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
                    GetComponent<Player>().RevertAllmutations();
                    PivotBras.transform.rotation = Quaternion.Euler(0, 0, 0);
                    isRight = true;
                }
                else if (movementInput.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    spriteRendererBras.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -180);
                    spriteRendererBras.gameObject.GetComponentInChildren<Transform>().rotation = Quaternion.Euler(0, 180, 0);
                    GetComponent<Player>().RevertAllmutations();
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
            if (shootInput && Time.time - cd > shootCD)
            {
                cd = Time.time;
                //Tirer
                GameObject g = GameObject.Instantiate(balle, new Vector3(Canon.transform.position.x, Canon.transform.position.y,1), Quaternion.Euler(0, 0, 0));
                g.transform.SetParent(gameObject.transform);
                ondes o = g.GetComponent<ondes>();
                GetComponent<AudioSource>().PlayOneShot(shootClip);

                if (!isKeyboard)
                {
                    o.direction = targetInput;
                }
                else
                {
                    if (isRight)
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
    }

    public void StopC()
    {
        StopCoroutine(DriftCoroutine());
        BoxCollider2D cl = GetComponent<BoxCollider2D>();
        cl.isTrigger = false;
        transform.rotation = startingRotation;
        isCoroutineRunning = false;
    }

    internal void DieAnimation()
    {
        StartCoroutine(DriftCoroutine());
    }
    private bool isCoroutineRunning = true;
    public GameObject targetObject;
    public Vector3 driftDirection;
    Quaternion startingRotation;
    private IEnumerator DriftCoroutine()
    {
        isCoroutineRunning = true;
        BoxCollider2D cl = GetComponent<BoxCollider2D>();
        cl.isTrigger = true;
        startingRotation = transform.rotation;
        float timeElapsed = 0f;
        driftDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        while (timeElapsed < 4f && isCoroutineRunning)
        {
            timeElapsed += Time.deltaTime;
            // Calculer la direction de dérive en fonction de la direction et de la vitesse
            Vector3 drift = driftDirection.normalized * 4f * Time.deltaTime;

            // Tourner dans le sens inverse de l'objet cible
            Quaternion inverseRotation = Quaternion.Inverse(transform.rotation);
            transform.Rotate(Vector3.left, 4f);

            // Déplacer le GameObject
            transform.position += drift;

            yield return null;
        }

        // Revenir à la rotation initiale à la fin de la coroutine
        transform.rotation = startingRotation;
        cl.isTrigger = false;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (GetComponent<Player>().confuse) movementInput *= -1;
        transform.Find("JetPack").GetComponent<SoundFade>().FadeIn();
    }

    public void Target(InputAction.CallbackContext context)
    {
        targetInput = context.ReadValue<Vector2>();
    }


    public void shoot(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValueAsButton();
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
