using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLevel;
    public string nextLevel;

    public int generatorsGoal;

    public bool gameFinished;
    public Chronometer timer;

    private int generatorsCounter;
    // Start is called before the first frame update
    void Start()
    {
        gameFinished = false;
        generatorsCounter = 0;
        timer.ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(currentLevel);
            timer.ResetTimer();
        }
    }

    public void TurnGenerator(bool state)
    {
        if (state)
        {
            generatorsCounter += 1;

            if (generatorsCounter == generatorsGoal)
            {
                //UI Manager. show victory screen

                StartCoroutine(GoToNextLevel());
                gameFinished = true;
                timer.StopTimer();
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
        //timer.ResetTimer();
        //timer.StartTimer();

    }
}
