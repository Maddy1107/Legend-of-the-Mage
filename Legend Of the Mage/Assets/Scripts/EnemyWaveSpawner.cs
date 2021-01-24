using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Waves
{
    public int enemyCount;
    public int enemySpawnInterval;
    public Transform[] enemy = new Transform[4];
}

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState { waitingToStart, inProgress, waitingToEnd }; 

    public List<Waves> waves = new List<Waves>();
    public Transform[] spawnpoints = new Transform[6];

    [SerializeField] Waves _waves;
    float timeBetweenWaves = 5f;
    public float waitTostartInterval;
    float nextspawntime;

    public int ongoingWavenumber = 0;
    int enemyIndex = 0;
    int WaveEnemyCount;
    float enemyspeed;
    float enemyfirerate;

    public List<int> enemiesselected = new List<int>(); 
       
    SpawnState state = SpawnState.waitingToStart;

    public Text waveStartCountdown;
    public TextMeshProUGUI WavenumberText;

    public GameObject CustomizeWave;

    public GameObject waveCompleted;

    PlayerMovement playerMovement;
    public EnemyScript enemyScript;
    WaterBlobScript waterBlobScript;

    public Button waterButton;
    public Button elecButton;
    public Button fireButton;
    public Button poisonButton;

    public Button enemyfireratebutton1;
    public Button enemyfireratebutton2;
    public Button enemyfireratebutton3;
    public Button enemyfireratebutton4;

    private void Start()
    {
        waitTostartInterval = timeBetweenWaves;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (GameManager.instance.gameplay == true && GameManager.instance.gameOver == false)
        {
            if (enemyIndex == 0)
            {
                playerMovement.playerSpellIndex = 0;
            }
            else if (enemyIndex == 1)
            {
                playerMovement.playerSpellIndex = 1;
            }
            else if (enemyIndex == 2)
            {
                playerMovement.playerSpellIndex = 2;
            }
            else if (enemyIndex == 3)
            {
                playerMovement.playerSpellIndex = 3;
            }
            if (waitTostartInterval <= 0)
            {
                if (state == SpawnState.waitingToStart)
                {
                    state = SpawnState.inProgress;
                    WaveEnemyCount = _waves.enemyCount;
                    waveStartCountdown.gameObject.SetActive(false);
                }
            }
            else
            {
                if (((int)waitTostartInterval) == 0)
                {
                    waveStartCountdown.text = "KILL ALL MONSTERS!!!";
                }
                else
                {
                    waveStartCountdown.text = ((int)waitTostartInterval).ToString();
                }
                waitTostartInterval -= Time.deltaTime;
                WavenumberText.enabled = true;
                waveStartCountdown.gameObject.SetActive(true);
                WavenumberText.SetText("WAVE" + (ongoingWavenumber).ToString());
            }
            if (state == SpawnState.inProgress && nextspawntime < Time.time)
            {
                SpawnEnemy();
            }
            else if (state == SpawnState.waitingToEnd)
            {
                if (checkEnemyAlive() == false)
                {
                    enemiesselected.Clear();
                    waveCompleted.SetActive(true);
                    WavenumberText.enabled = false;
                    GameManager.instance.pauseGame();
                    elecButton.interactable = true;
                    fireButton.interactable = true;
                    poisonButton.interactable = true;
                    waterButton.interactable = true;
                    waves.Remove(waves[0]);
                    FindObjectOfType<AudioManager>().Stop("MainGame");
                    FindObjectOfType<AudioManager>().Play("MenuTheme");
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        Transform randomspawnPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        if (enemiesselected.Count == 0)
        {
            enemyIndex = 0;
        }
        else
        {
            enemyIndex = enemiesselected[Random.Range(0, enemiesselected.Count)];
        }
        if (enemyIndex == 1)
        {
            WaveEnemyCount = 0;
            state = SpawnState.waitingToEnd;
        }
        Transform spawnedEnemy = Instantiate(_waves.enemy[enemyIndex], randomspawnPoint.transform.position, Quaternion.identity);

        if (enemyIndex == 0 || enemyIndex == 2)
        {
            enemyScript = spawnedEnemy.GetComponent<EnemyScript>();
        }
        else if(enemyIndex == 3)
        {
            waterBlobScript = spawnedEnemy.GetComponent<WaterBlobScript>();
        }
        /*if (enemyIndex == 0)
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy/FireGolem").GetComponent<EnemyScript>();
        }
        else if (enemyIndex == 1)
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy/OneEyeAlien").GetComponent<EnemyScript>();
        }
        else if (enemyIndex == 2)
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy/SpaceShip").GetComponent<EnemyScript>();
        }
        else if (enemyIndex == 3)
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy/WaterBlob").GetComponent<EnemyScript>();
        }*/
        if (enemyScript != null && waterBlobScript == null)
        {
            if (enemyspeed == 5)
            {
                enemyScript.movespeed = 5;
            }
            else if (enemyspeed == 6)
            {
                enemyScript.movespeed = 6;
            }
            else if (enemyspeed == 7)
            {
                enemyScript.movespeed = 7;
            }
            else if (enemyspeed == 8)
            {
                enemyScript.movespeed = 5;
            }

            if (enemyfirerate == 5)
            {
                enemyScript.enemyFirerate = 1;
            }
            else if (enemyfirerate == 6)
            {
                enemyScript.enemyFirerate = 2;
            }
            else if (enemyfirerate == 7)
            {
                enemyScript.enemyFirerate = 3;
            }
            else if (enemyfirerate == 8)
            {
                enemyScript.enemyFirerate = 4;
            }
        }
        else if(enemyScript == null && waterBlobScript != null)
        {
            if (enemyspeed == 5)
            {
                waterBlobScript.movespeed = 5;
            }
            else if (enemyspeed == 6)
            {
                waterBlobScript.movespeed = 6;
            }
            else if (enemyspeed == 7)
            {
                waterBlobScript.movespeed = 7;
            }
            else if (enemyspeed == 8)
            {
                waterBlobScript.movespeed = 5;
            }
        }
        WaveEnemyCount--;
        nextspawntime = Time.time + _waves.enemySpawnInterval;
        if (WaveEnemyCount == 0)
        {
            state = SpawnState.waitingToEnd;
        }
    }

    private bool checkEnemyAlive()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy/WaterBlob").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy/SpaceShip").Length == 0 
            && GameObject.FindGameObjectsWithTag("Enemy/FireGolem").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy/OneEyeAlien").Length == 0
            && GameObject.FindGameObjectsWithTag("Enemy/OneEyeAlienMax").Length == 0)
        {
            return false;
        }
        return true;
    }

    public void AddnewWave()
    {
        waves.Add(_waves);
        ongoingWavenumber += 1;
        waitTostartInterval = timeBetweenWaves;
        state = SpawnState.waitingToStart;
        CustomizeWave.SetActive(false);
        GameManager.instance.startgame();
        //_waves = waves[ongoingWavenumber];
    }

    void enemiesListCheck(int enemyindexchosen)
    {
        if(enemiesselected.Count <= 1)
        {
            enemiesselected.Add(enemyindexchosen);
        }
    }

    public void chooseWater()
    {
        enemiesListCheck(0);
        elecButton.interactable = false;
        fireButton.interactable = false;
        poisonButton.interactable = false;
    }

    public void chooseFire()
    {
        enemiesListCheck(3);
        elecButton.interactable = false;
        poisonButton.interactable = false;
        waterButton.interactable = false;
        enemyfireratebutton1.interactable = false;
        enemyfireratebutton2.interactable = false;
        enemyfireratebutton3.interactable = false;
        enemyfireratebutton4.interactable = false;
    }

    public void chooseelectric()
    {
        enemiesListCheck(2);
        fireButton.interactable = false;
        poisonButton.interactable = false;
        waterButton.interactable = false;
    }

    public void choosepoison()
    {
        enemiesListCheck(1);
        elecButton.interactable = false;
        fireButton.interactable = false;
        waterButton.interactable = false;
    }

    //Player Fire rate choices
    public void choice1()
    {
        playerMovement.cooldownTime = 1;
    }

    public void choice2()
    {
        playerMovement.cooldownTime = 2;
    }

    public void choice3()
    {
        playerMovement.cooldownTime = 3;
    }

    public void choice4()
    {
        playerMovement.cooldownTime = 4;
    }

    //enemy speed choices
    public void enemySpeedchoice1()
    {
        enemyspeed = 5;
    }

    public void enemySpeedchoice2()
    {
        enemyspeed = 6;
    }

    public void enemySpeedchoice3()
    {
        enemyspeed = 7;
    }

    public void enemySpeedchoice4()
    {
        enemyspeed = 8;
    }

    //enemy firerate choices
    public void enemyfireratechoice1()
    {
        enemyfirerate = 5;
    }

    public void enemyfireratechoice2()
    {
        enemyfirerate = 6;
    }

    public void enemyfireratechoice3()
    {
        enemyfirerate = 7;
    }

    public void enemyfireratechoice4()
    {
        enemyfirerate = 8;
    }
}
