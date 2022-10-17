using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int secondsToFadeOut = 3;
    public AudioSource audio;

    IEnumerator FadeAudioAndLoadScene()
    {
        while (audio.volume > 0.01f)
        {
            audio.volume -= Time.deltaTime / secondsToFadeOut;
            yield return null;
        }

        audio.volume = 0;
        audio.Stop();

        SceneManager.LoadScene("Battlefield");
    }

    public void StartGame()
    {
        StartCoroutine(FadeAudioAndLoadScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
