/* Tetris created by A.j. Krienke starting on June 04 2020
 * initial code was from tutorials, refinements came through research
 from unity.com API pages */

using UnityEngine.UI;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint; //sets the rotation point of the tetromino prefabs
    float previousTime; // to calculate the time elapsed since last frame
    float keyTimer; // Variable to calculate how long a key has been held dowm for GetKey function
    const int PIECE_SCORE = 1; // Set score for landing individual pieces
    const int LINE_SCORE = 10; // Set score for completing a ful line
    const float KEY_DELAY = 0.05f; // Set delay between key frames when key is held down for GetKey function
    [HideInInspector]
    public float fallTimer; // Set delay between frames, initially 1 second, speed has 9 steps with 0.1 being max speed
    float speed;
    const int HEIGHT = 20; // Height of the playing field
    const int WIDTH = 10; // Width of the playing field
    static Transform[,] grid = new Transform[WIDTH, HEIGHT]; // Array to store landed pieces

    void Start() // Initialize neccessary variables
    {
        fallTimer = 1.0f;
        keyTimer = 0;
        previousTime = Time.deltaTime;
    }

    void Update() //Get User input at the beginning of each frame.
    {
        speed = (FindObjectOfType<SpawnTetromino>().speed / 10.0f);
        if (Input.GetKeyDown("left"))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown("right"))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown("space"))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        else if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Time.time - previousTime > fallTimer - speed)
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
                FindObjectOfType<SpawnTetromino>().Score(PIECE_SCORE);
            }
        }

        if (Input.GetKey("down"))
        {
            keyTimer += Time.deltaTime;
            if (keyTimer > KEY_DELAY)
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                }
                keyTimer = 0;
            }
        }

        void CheckForLines() // Checks for full lines and deletes them from grid
        {
            for (int i = HEIGHT - 1; i >= 0; --i)
            {
                if (HasLine(i)) // calls function on x axis (lines)
                {
                    DeleteLine(i);
                    RowDown(i);
                }
            }
        }
    } // End of Update Function

    bool HasLine(int i) // returns false if there are any gaps in the X axis of the grid
    {
        for (int j = 0; j < WIDTH; ++j)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true; // Informs CheckLines function that line is complete
    }

    void DeleteLine(int i) // Removes line on the X axis of the grid
    {

        FindObjectOfType<SpawnTetromino>().Score(LINE_SCORE);
        for (int j = 0; j < WIDTH; ++j)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) // Moves the lines that have not been deleted down
    {
        for (int y = i; y < HEIGHT; ++y)
        {
            for (int j = 0; j < WIDTH; ++j)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid() // Tracks all of the tetrominos individual pieces that are in play 
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
            CheckEndGame();
        }
    }

    void CheckEndGame()
    {
        for (int j = 0; j < WIDTH; ++j)
        {
            if (grid[j, HEIGHT - 3] != null) // Check to see if there are any blocks in the highest row
            {
                FindObjectOfType<SpawnTetromino>().GameOver();
            }
        }
    }

    bool ValidMove() // Checks that no pieces of the tetrominos are outside the boundaries of the grid
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= WIDTH || roundedY < 0 || roundedY >= HEIGHT)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
}