using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationIcon : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite("MutationIcons/Cercle2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadSprite(string imagePath) 
    /// Sprites have to be in the folder Resources/MutationIcons
    /// Call LoadSprite("MutationIcons/Mutation1") if you want to call Mutation1.png
    {
    Image mutationIcon = GetComponent<Image>();
    Sprite sprite = Resources.Load<Sprite>(imagePath);
    mutationIcon.sprite = sprite;
    }
}
