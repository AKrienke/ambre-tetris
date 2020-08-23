using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;
    [HideInInspector]
    public static int finalScore;
    public Text finalScoreDisplay;

    void Start () {
        finalScoreDisplay.text = "Your final score is:\n\n" + finalScore.ToString();
		Button btn1 = restartButton.GetComponent<Button>();
		btn1.onClick.AddListener(Restart);
        Button btn2 = quitButton.GetComponent<Button>();
		btn2.onClick.AddListener(Quit);
	}

	void Restart(){
		SceneManager.LoadScene("Main Screen");
	}

    void Quit()
    {
        Application.Quit();
    }

    void FinalScore()
    {
        
    }
}
