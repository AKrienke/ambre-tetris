// Tetris created by A.j. Krienke starting on June 04 2020

using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTetromino : MonoBehaviour
{
    GameObject displayTetromino;
    GameObject activeTetromino;
    int score;
    [HideInInspector]
    public int speed;
    public GameObject[] Tetrominos;
    public Text scoreDisplay;
    public Text speedDisplay;
    bool activeGame = false;
    Vector2 activeTetrominoPosition = new Vector2(4.5f, 18.5f);

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        speed = 1;
        NewTetromino();
        DisplayHandler();
        ChangeThemeSong("slowGame");
    }

    void LateUpdate()
    {
        DisplayHandler();
        AdjustSpeed();
    }

    public void NewTetromino()
    {
        if (!activeGame) {
            activeGame = true;
            activeTetromino = Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], activeTetrominoPosition, Quaternion.identity);
            displayTetromino = Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], transform.position, Quaternion.identity);
            displayTetromino.GetComponent<TetrisBlock>().enabled = false;
        } else {
            displayTetromino.transform.localPosition = activeTetrominoPosition;
            activeTetromino = displayTetromino;
            activeTetromino.GetComponent<TetrisBlock>().enabled = true;
            displayTetromino = Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], transform.position, Quaternion.identity);
            displayTetromino.GetComponent<TetrisBlock>().enabled = false;
        }
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

    void ChangeThemeSong(string theme)
    {

        FindObjectOfType<AudioManager>().Play(theme);
    }
    public void GameOver()
    {
        RestartScript.finalScore = score;
        SceneManager.LoadScene("GameOver");
    }
}
