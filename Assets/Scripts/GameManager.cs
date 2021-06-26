using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextLevel;

    public int generatorsGoal;

    public bool gameFinished;

    private int generatorsCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameFinished = false;
        generatorsCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnGenerator(bool state)
    {
        if (state)
        {
            generatorsCounter += 1;
            
            if(generatorsCounter == generatorsGoal)
            {
                //UI Manager. show victory screen

                StartCoroutine(GoToNextLevel());
                gameFinished = true;
            }
        }
        else
        {
            generatorsCounter -= 1;
        }
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(nextLevel);
    }
}
