using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] backgrounds;

    [SerializeField]
    GameObject[] obstacles;

    [SerializeField]
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        float value = Random.value;
        Debug.Log(value);
        if (value < 0.5f)
        {
            // Espace
            gameManager.setObstacles(1);
            GetComponent<SpriteRenderer>().sprite = backgrounds[0];
        }
        else
        {
            // Spaceship
            gameManager.setObstacles(2);
            GetComponent<SpriteRenderer>().sprite = backgrounds[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
