using UnityEngine;
using System.Collections;
/// <summary>
/// Is attached to networkManager object in the editor.
/// 
/// This script controls networking.
/// </summary>
public class NetworkManager : MonoBehaviour
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
    private string serverNameForClient;

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
        // Start the server
        Network.InitializeServer(numberOfPlayers, connectionPort, useNAT);

        // Take the serverName input to the registry for later recall.
        serverName = serverNameInput.text;
        PlayerPrefs.SetString("serverName", serverName);

        Application.LoadLevel(1);
    }

    void OnLevelWasLoaded()
    {
        networkView.RPC("SpawnPlayer", RPCMode.AllBuffered);
    }

    // This function is accessed by "Join" button in Join Game menu.
    void JoinGame()
    {
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

        // Actually connect to the server and load level1.
        Network.Connect(connectToIP, connectionPort);
        Application.LoadLevel(1);

    }

    [RPC]
    void SpawnPlayer()
    {
        Network.Instantiate(player, Vector3.zero, Quaternion.identity, 0);
    }
}
