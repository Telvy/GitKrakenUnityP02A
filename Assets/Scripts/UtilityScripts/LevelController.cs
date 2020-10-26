using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView = null;
    [SerializeField] GameObject _pauseMenu = null;
    [SerializeField] GunScript _GunScript = null;

    int _currentScore;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        reloadLevel();
        //Increase Score
        //TODO replace with real implementation later
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    IncreaseScore(5);
        //}

        //Exit Level
        //TODO bring up popup menu for navigation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            lockPause();
        }
    }

    void reloadLevel()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex);
        }
    }

    public void unlockPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _GunScript.gameIsPaused = false;
    }

    public void lockPause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _GunScript.gameIsPaused = true;
    }

    public void ExitLevel()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            //save current score as new high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score:" + _currentScore);
        }

        //load new level
        SceneManager.LoadScene("MainMenu");

        //unlock mouse
        Cursor.lockState = CursorLockMode.None;

        
    }

    public void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display, so we can see the new score
        _currentScoreTextView.text = "Score:" + _currentScore.ToString();
    }

}
