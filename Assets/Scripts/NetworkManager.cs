using UnityEngine;
using System.Collections;
/// <summary>
/// Is attached to networkManager object in the editor.
/// 
/// This script controls networking.
/// </summary>
public class NetworkManager : uLink.MonoBehaviour
{

    public UILabel serverNameInput;
    public UILabel IPAdressInput;
    public UILabel PortNumberInput;
    public UILabel PlayerNameInput;
    
    private string connectToIP = "127.0.0.1";
    private int connectionPort = 26500;
    private bool useNAT = false;
    private int numberOfPlayers = 2;

    private string playerName;
    private string serverName;
    private string gameName;

    public GameObject player;

    //private bool iWantToSetUpAServer = false;
    //private bool iWantToConnectToAServer = false;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        
	}

    // This function is accessed by the "Create" button in Host a Game menu.
    void StartServer()
    {
        uLink.MasterServer.ipAddress = "127.0.0.1";
        uLink.MasterServer.port = 23466;
        // Start the server
        uLink.Network.InitializeServer(numberOfPlayers, connectionPort);

        // Register the server to the Master Server.
        
        uLink.MasterServer.RegisterHost("UniqueGame", "gameisteAmk", "coopDeneme");

        // Take the serverName input to the registry for later recall.
        serverName = serverNameInput.text;
        PlayerPrefs.SetString("serverName", serverName);

        Application.LoadLevel(1);
    }

    void uLink_OnMasterServerEvent(uLink.MasterServerEvent mse)
    {
        if(mse == uLink.MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server connected");

        
    }


    void OnLevelWasLoaded()
    {
        //networkView.RPC("SpawnPlayer", uLink.RPCMode.AllBuffered);
        uLink.Network.Instantiate(player, Vector3.zero, Quaternion.identity, 0);

    }


    // This function is accessed by "Join" button in Join Game menu.
    void JoinGame()
    {
        /*
        // Get the inputs to variables and also to registry
        connectToIP = IPAdressInput.text;
        PlayerPrefs.SetString("IPAddress", connectToIP);

        connectionPort = int.Parse(PortNumberInput.text);
        PlayerPrefs.SetString("portNumber", connectionPort.ToString());

        playerName = PlayerNameInput.text;
        // Check if there has been given anything for player name.
        if (playerName == "")
            playerName = "Player";

        PlayerPrefs.SetString("playerName", playerName);
        */
        // Actually connect to the server and load level1.

        // Get the list of servers

        


        //Network.Connect(connectToIP, connectionPort);
        Application.LoadLevel(1);

    }



    void SearchGames()
    {
        
    }
    /*
    [RPC]
    void SpawnPlayer()
    {
        uLink.Network.Instantiate(player, Vector3.zero, Quaternion.identity, 0);
    }*/
}
