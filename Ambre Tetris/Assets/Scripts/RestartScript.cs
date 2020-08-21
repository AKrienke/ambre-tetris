using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;
    void Start () {
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
}
