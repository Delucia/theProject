using UnityEngine;
using System.Collections;

public class GUIManager : Photon.MonoBehaviour
{

    // Tweemers in the menu
    public UITweener HostGameTween;
    public UITweener JoinGameTween;
    public UITweener MainMenuTween;
    
    public GameObject[] menuPanels;

    
    void Start () 
    {

	}

    private void Update()
    {

    }

	
    // Button functions for menu transitions
    // Host A Game button
    void HostGame()
    {
        reset();
        menuPanels[2].active = false;
        MainMenuTween.Play(true);

        // Change to state I want to create a room so that the CreateRoom function works.
        Connection network = GetComponent("Connection") as Connection;
        network.iWantToCreateRoom = true;
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
        
        // Get the list of available rooms when hit Join a Game button. We will then update the list on Gui later.
        Connection network = GetComponent("Connection") as Connection;
        network.GetRoomList();
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
