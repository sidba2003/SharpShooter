using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject gameOverUI;

    int PlayerDeathTransitionCameraPriority = 20;
    public static PlayerHealth instance;
    Slider healthSlider;
    bool coroutineRunning = false;
    bool gameEnded = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        instance = this;

        healthSlider = GetComponent<Slider>();
        healthSlider.value = 1;
    }

    private void Update()
    {
        if (gameEnded && !coroutineRunning) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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

            coroutineRunning = true;
            gameEnded = true;
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

        coroutineRunning = false;
        yield return null;
    }
}
