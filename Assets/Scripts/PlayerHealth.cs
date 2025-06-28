using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    Slider healthSlider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = 1;
    }

    public void TakeDamage(float amount)
    {
        healthSlider.value = healthSlider.value + (amount / 100);
    }
}
