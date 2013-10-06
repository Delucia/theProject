using System;
using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour
{

    public GameObject player;


    void Awake()
    {
        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
            PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity, 0);
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        
	}

    


}
