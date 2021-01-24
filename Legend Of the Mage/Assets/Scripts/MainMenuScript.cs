using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.startgame();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
