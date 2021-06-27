using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public bool isLevel = false;


    private void Awake()
    {
        if (isLevel) StartCoroutine(LoadLevelText());
    }
    public void PlayButtonPressed()
    {
        StartCoroutine(LoadLevel("Level-1"));        
    }

    public void AboutButtonPressed()
    {
        
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

    IEnumerator LoadLevelText()
    {
        transition.SetBool("IsLevel", true);
        yield return new WaitForSeconds(transitionTime);
        
    }
}
