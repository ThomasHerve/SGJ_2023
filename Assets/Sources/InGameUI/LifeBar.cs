using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public GameObject spritebg,spriteft,spritedead;
    

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
       slider.value = health;

       fill.color = gradient.Evaluate(slider.normalizedValue);
        if (health != 0)
        {
            spritebg.SetActive(true);
            spriteft.SetActive(true);
            spritedead.SetActive(false);
        }
        else
        {
            spritebg.SetActive(false);
            spriteft.SetActive(false);
            spritedead.SetActive(true);
        }
    }

    void Start()
    {
        SetMaxHealth(100);
    }

}
