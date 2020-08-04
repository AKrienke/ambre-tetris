/* Tetris created by A.j. Krienke starting on June 04 2020
 * This code is mostly from research on unity.com API pages */

using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTetromino : MonoBehaviour
{
    GameObject tetromino;
    int score;
    [HideInInspector]
    public int speed;
    public GameObject[] Tetrominos;
    public Text scoreDisplay;
    public Text speedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        speed = 1;
        NewTetromino();
        DisplayHandler();
    }

    void Update()
    {
        DisplayHandler();
        AdjustSpeed();
    }

    public void NewTetromino()
    {
        tetromino = Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], transform.position, Quaternion.identity);
    }

    public void Score(int addedScore)
    {
        score += addedScore;
    }

    void DisplayHandler()
    {
        scoreDisplay.text = "Score: " + score.ToString();
        speedDisplay.text = "Speed: " + speed.ToString();
    }

    void AdjustSpeed()
    {
        if (score > 400) { speed = 9; }
        else if (score > 350) { speed = 8; }
        else if (score > 300) { speed = 7; }
        else if (score > 250) { speed = 6; }
        else if (score > 200) { speed = 5; }
        else if (score > 150) { speed = 4; }
        else if (score > 100) { speed = 3; }
        else if (score > 50) { speed = 2; }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
