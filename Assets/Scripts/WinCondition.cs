using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [SerializeField] Transform TurretsFolder;
    [SerializeField] Transform PortalsFolder;
    [SerializeField] Image FadeOutScreen;
    [SerializeField] GameObject GameWon;
    [SerializeField] float FadeOutScreenDuration;
    [SerializeField] Transform RobotsFolder;
    [SerializeField] GameObject Player;

    bool gameWon = false;
    bool coroutineRunning = false;
    bool coroutineStarted = false;

    private void Update()
    {
        int turretsLeft = TurretsFolder.childCount;
        int portalsLeft = PortalsFolder.childCount;
        int robotsLeft = RobotsFolder.childCount;

        if (turretsLeft == 0 && portalsLeft == 0 && robotsLeft == 0 && !coroutineStarted)
        {
            gameWon = true;
            coroutineRunning = true;
            coroutineStarted = true;

            StartCoroutine(ScreenBlackOut());
        }

        checkScreenBlackedOut();
    }

    void checkScreenBlackedOut()
    {
        if (gameWon && !coroutineRunning)
        {
            Destroy(Player);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameWon.SetActive(true);
        }
    }

    IEnumerator ScreenBlackOut()
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
