using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

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
    
    }

    void Start()
    {
        SetMaxHealth(100);
        //SetHealth(55);
    }

}
