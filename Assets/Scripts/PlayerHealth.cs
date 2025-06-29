using Cinemachine;
using System.Collections;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera deathCamera;
    [SerializeField] GameObject player;
    [SerializeField] GameObject weaponCamera;
    [SerializeField] Image FadeOutScreen;
    [SerializeField] float FadeOutScreenDuration;
    [SerializeField] GameObject enemiesParent;
    [SerializeField] GameObject portalsParent;

    int PlayerDeathTransitionCameraPriority = 20;
    public static PlayerHealth instance;
    Slider healthSlider;

    private void Awake()
    {
        instance = this;

        healthSlider = GetComponent<Slider>();
        healthSlider.value = 1;
    }

    public void TakeDamage(float amount)
    {
        healthSlider.value = healthSlider.value + (amount / 100);
        CheckPlayerHealth();
    }

    void CheckPlayerHealth()
    {
        if (healthSlider.value == 0)
        {
            Destroy(enemiesParent);
            Destroy(portalsParent);

            weaponCamera.transform.SetParent(null);
            Destroy(player);

            StartCoroutine(FadeOutScreenCoroutine());
            deathCamera.Priority = PlayerDeathTransitionCameraPriority;
        }
    }

    public float GetPlayerHealth()
    {
        return healthSlider.value;
    }

    IEnumerator FadeOutScreenCoroutine()
    {
        float CurrentFadeOutTime = 0f;

        Color fadeOutColor = FadeOutScreen.color;

        while (CurrentFadeOutTime < FadeOutScreenDuration)
        {
            fadeOutColor.a = Mathf.Lerp(0, 1f, CurrentFadeOutTime / FadeOutScreenDuration);
            FadeOutScreen.color = fadeOutColor;

            CurrentFadeOutTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}
