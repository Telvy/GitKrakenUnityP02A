using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong = null;
    [SerializeField] Text _highScoreTextView = null;

    //Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        //load high score display
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();


        if(_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }

    public void clearScoreData()
    {
        int resetScore = 0;
        _highScoreTextView.text = resetScore.ToString();
        PlayerPrefs.SetInt("HighScore", resetScore);
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Escape");
    }

}
