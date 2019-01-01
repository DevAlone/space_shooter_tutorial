using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public uint numberOfHazards;
    public float waitBetweenSpawning;
    public float waitBetweenWaves;
    public float waitBeforeStart;
    public float hazardsSpeed;
    public Text scoreText, restartText, gameOverText;

    private bool isGameOver;
    private bool canBeRestarted;
    private uint score;

    void Start()
    {
        isGameOver = false;
        canBeRestarted = false;
        restartText.text = gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (canBeRestarted)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(waitBeforeStart);

        while (true)
        {
            for (uint i = 0; i < numberOfHazards; ++i)
            {
                var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                var spawnRotation = Quaternion.identity;
                var hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, spawnRotation);
               
                yield return new WaitForSeconds(waitBetweenSpawning);
            }

            yield return new WaitForSeconds(waitBetweenWaves);
            ++numberOfHazards;
            // ++hazardsSpeed;

            if (isGameOver)
            {
                restartText.text = "Press 'R' to restart";
                canBeRestarted = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(uint value)
    {
        score += value;
        UpdateScore();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.text = "Game Over";
    }
}
