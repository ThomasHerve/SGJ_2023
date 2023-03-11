using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int vie = 100;
    public int score = 0;
    public string color = "red";
    public int mutationMin = 30;
    public int mutationMax = 49;
    [Tooltip("Valeurs mutation")]
    public int healMutationValue;
    public int spikesDamage;


    // Mutation values
    private int mutationCurrentValue = 0;
    private int mutationValue;

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
    private Mutation[] currentMutations = new Mutation[2] { new Mutation { index = -1 } , new Mutation { index = -1 } };



    public void SetNextMutationValues()
    {
        mutationCurrentValue = 0;
        mutationValue = Random.Range(mutationMin, mutationMax);
    }

    public void RevertAllmutations()
    {
        foreach (Mutation mutation in mutations)
        {
            if(mutation.index == 0 || mutation.index == 1)
                mutation.effectBody.transform.rotation = Quaternion.Euler(0, transform.rotation.z, 0);
        }
    }

    void SetSwitchMutation(int num, bool value)
    {
        switch (mutations[num].index)
        {
            case 0:
                confuse = value;
                mutations[num].effectBody.gameObject.SetActive(value);
                break;
            case 1:
                sleep = value;
                mutations[num].effectBody.gameObject.SetActive(value);
                break;
            case 2:
                armor = value;
                mutations[num].effectBody.gameObject.SetActive(value);
                mutations[num].effectArms.gameObject.SetActive(value);
                break;
            case 3:
                corrosion = value;
                mutations[num].effectBody.gameObject.SetActive(value);
                mutations[num].effectArms.gameObject.SetActive(value);
                break;
            case 4:
                mutations[num].effectBody.gameObject.SetActive(value);
                if(value)StartCoroutine(HealMutation(num));
                break;
            case 5:
                spikes = value;
                mutations[num].effectBody.gameObject.SetActive(value);
                break;
        }

    }

    public void RemoveMutation(int num)
    {
        if (currentMutations[num].index == -1) return;
        SetSwitchMutation(currentMutations[num].index, false);
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
    public bool armor = false;
    public bool corrosion = false;
    public bool spikes = false;

    // Mutation functions
    System.Collections.IEnumerator HealMutation(int num)
    {
        Heal(healMutationValue);
        yield return new WaitForSeconds(2);
        RemoveMutation(0);
        RemoveMutation(1);
    }

    [SerializeField]
    AudioClip[] voiceClips;

    bool isVoicePlaying = false;

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
        SetNextMutationValues();

        // DEBUG
        //AddMutation(5);
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
        SetNextMutationValues();
        RemoveMutation(0);
        RemoveMutation(1);
        if (characterControler != null)
        {
            characterControler.isdead = false;
            characterControler.StopC();
        }
        if (lifebar != null)
            lifebar.SetHealth(vie);
    }

    public void TakeDamage(int damage)
    {
        vie -= damage;
        if (vie < 0) { vie = 0;}
        if (vie == 0) Die();
        lifebar.SetHealth(vie);
        if (!isVoicePlaying)
        {
            isVoicePlaying = true;
            GetComponent<AudioSource>().PlayOneShot(voiceClips[UnityEngine.Random.Range(0, voiceClips.Length)]);
            StartCoroutine(WaitDelayForVoice(6f));
        }
    }
    IEnumerator WaitDelayForVoice(float delay)
    {
        yield return new WaitForSeconds(delay);
        isVoicePlaying = false;
    }

    public void TakeMutation(int value)
    {
        mutationCurrentValue += value;
        if (mutationCurrentValue >= mutationValue) {
            List<int> indexs = new List<int>();
            for(int i = 0; i < mutations.Length; i++)
            {
                if (currentMutations[0].index != mutations[i].index && currentMutations[1].index != mutations[i].index)
                    indexs.Add(i);
                    
            }
            AddMutation(indexs[Random.Range(0, indexs.Count)]);
            //AddMutation(5);
        }
    }
    
    public void TakeDamage(int damage,GameObject shooter)
    {
        vie -= damage;
        if (vie < 0) { vie = 0; }
        if (vie == 0) Die();
        lifebar.SetHealth(vie);
    }

    public void Heal(int value)
    {
        if(vie <= 0) return;
        if (vie + value >= 100) vie = 100;
        else vie += value;
        lifebar.SetHealth(vie);
    }

    private void Die()
    {
        characterControler.DieAnimation();
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
