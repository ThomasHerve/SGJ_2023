using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{

    public Sprite caisse, aste;
    
    bool isAstero = true;

    public void change()
    {
        if (isAstero)
        {
            GetComponent<Image>().sprite = caisse;
            BackgroundManager.id = 2;
        }
        else
        {
            GetComponent<Image>().sprite = aste;
            BackgroundManager.id = 1;
        }
        isAstero = !isAstero;
    }
}
