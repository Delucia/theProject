using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    // TextFields in the menu
    public UILabel serverNameText;
    public UILabel IPAddressText;
    public UILabel portText;
    public UILabel playerNameText;

    public UITweener HostGameTween;
    public UITweener JoinGameTween;
    public UITweener MainMenuTween;
    
    public GameObject[] menuPanels;

    void Start () 
    {
	    // Load Main Menu at start
        //reset();
        //MainMenu();

        // Load last used inputs from registry.
        serverNameText.text = PlayerPrefs.GetString("serverName");
        IPAddressText.text = PlayerPrefs.GetString("IPAddress");
        portText.text = PlayerPrefs.GetString("portNumber");
        playerNameText.text = PlayerPrefs.GetString("playerName");
	}
	
	void Update () 
    {
	    
	}


    
    // Button functions for menu transitions
    void MainMenu()
    {
        //reset();
        //menuPanels[0].active = true;
    }

    // Host A Game button
    void HostGame()
    {
        reset();
        menuPanels[2].active = false;
        MainMenuTween.Play(true);
    }

    void BackToMainMenu()
    {
        MainMenuTween.Play(false);
    }

    // Join a Game button
    void JoinGame()
    {
        reset();
        menuPanels[1].active = false;
        MainMenuTween.Play(true);
    }

    // Sets all panel.active to true.
    void reset()
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            menuPanels[i].active = true;
        }
    }
}
