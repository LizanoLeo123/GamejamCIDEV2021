using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource inGameMusic;

    public static bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenuUI;

    public Animator transition;
    public float transitionTime = 1f;
    public string levelName;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        inGameMusic.Play();
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        inGameMusic.Stop();
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {                
        GameIsPaused = false;
        StartCoroutine(LoadLevel(levelName));
    }

    public void GoGome()
    {
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        StartCoroutine(LoadLevel("MainMenu"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }
}
