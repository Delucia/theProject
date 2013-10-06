using UnityEngine;
using System.Collections;
/// <summary>
/// Is attached to Connection object in the editor.
/// 
/// This script is used to setup connection(Create or join Rooms).
/// </summary>
public class Connection : Photon.MonoBehaviour
{

    public UILabel roomNameInput;
    public UILabel playerNameInput;
    public UILabel RoomList;
    
    private string playerName;
    private string gameName;

    private string roomName;
    private int maxPlayers = 2;

    public bool iWantToCreateRoom = false;

	void Start ()
	{
        // Load last used inputs from registry.
        roomNameInput.text = PlayerPrefs.GetString("roomName");
        playerNameInput.text = PlayerPrefs.GetString("playerName");
        
        //Connect to photon cloud
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("1");
	}
	
	void Update () 
    {
        
	}


    // This function is accessed by the "Create" button in Host a Game menu.
    public void CreateRoom()
    {
        // Take the serverName input to the registry for later recall.
        roomName = roomNameInput.text;
        PlayerPrefs.SetString("roomName", roomName);

        if (iWantToCreateRoom)
        {
            PhotonNetwork.CreateRoom(roomName, true, true, maxPlayers);
        }
    }
    
    // This function is accessed by "Join a Game" button in Multiplayer menu. <<<Via GUIManager Class>>>
    public void GetRoomList()
    {
        // Write the input to registry for later use.
        playerName = playerNameInput.text;
        // Check if there has been given anything for player name.
        if (playerName == "")
            playerName = "Player";

        PlayerPrefs.SetString("playerName", playerName);
        //-----------------------------------------------------

        // Get the list of servers
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();

        // Write room infos into button labels
        if (roomList.Length == 0)
            RoomList.text = "No Rooms Available";
        else
        {
            RoomList.text = roomList[0].name + "         " +  roomList[0].playerCount + "/" + roomList[0].maxPlayers;
        } 
    }
    

    void JoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    
    void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    void OnlevelWasLoaded()
    {
        //PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 1);
        Debug.Log("level loaded");
    }

}
