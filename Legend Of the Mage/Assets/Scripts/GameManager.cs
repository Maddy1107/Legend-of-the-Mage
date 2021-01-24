using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject GameoverUI;
    public TextMeshProUGUI noOfWaves;
    EnemyWaveSpawner enemyWaveSpawner;
    public TextMeshProUGUI Wavenumber;

    public bool gameplay { get; private set; }
    public bool gameOver { get; private set; }

    private void Start()
    {
        instance = this;
        gameplay = false;
        gameOver = false;
        enemyWaveSpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemyWaveSpawner>();
    }

    private void Update()
    {
        if (GameManager.instance.gameOver == true && GameManager.instance.gameplay == false)
        {
            GameoverUI.gameObject.SetActive(true);
            noOfWaves.SetText((enemyWaveSpawner.ongoingWavenumber - 1).ToString() + "WAVES");
            
        }
    }

    public void startgame()
    {
        gameplay = true;
    }

    public void pauseGame()
    {
        gameplay = false;
    }

    public void endgame()
    {
        gameplay = false;
        gameOver = true;
        Wavenumber.enabled = false;
        FindObjectOfType<AudioManager>().Stop("MainGame");
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
}
