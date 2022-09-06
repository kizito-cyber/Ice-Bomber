using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int modeNum;

    private void Start()
    {
       PlayerPrefs.SetInt("modeIndex", 1);
    }
    public void SetMode(int modeValue)
    {
        modeNum = modeValue;
        PlayerPrefs.SetInt("modeIndex", modeNum);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("modeIndex"));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
