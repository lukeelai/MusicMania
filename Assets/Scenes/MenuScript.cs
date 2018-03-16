using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas options;
    public Button startText;
    public Button exitText;

	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        options = options.GetComponent<Canvas>();

        options.enabled = false;
        quitMenu.enabled = false;
    }
    
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
        options.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
        options.enabled = false;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("scene3");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void optionPress()
    {
        options.enabled = true;
    }
}
