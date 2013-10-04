using UnityEngine;
using System.Collections;

public class GUIManager : uLink.MonoBehaviour
{
    // TextFields in the menu
    public UILabel serverNameText;
    public UILabel IPAddressText;
    public UILabel portText;
    public UILabel playerNameText;
    public UILabel joinButtonText;

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
        
        if (uLink.MasterServer.PollHostList().Length != 0)
        {
            uLink.HostData[] hostData = uLink.MasterServer.PollHostList(); 
            int x = hostData.Length;
            Debug.Log(x);
            Debug.Log(hostData[0]);
            // List the servers at the menu.
            foreach (uLink.HostData n in hostData)
            {
                string line = n.gameName + " " + n.connectedPlayers + " / " + n.playerLimit;
                //joinButtonText.text = line;
            }
        }
	}


    
    // Button functions for menu transitions
    // Host A Game button
    void HostGame()
    {
        reset();
        menuPanels[2].active = false;
        MainMenuTween.Play(true);
    }

    // Back button
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

        // Server List
        uLink.MasterServer.ClearHostList();
        uLink.MasterServer.RequestHostList("UniqueGame");
        
        
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
