﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery {

    private bool _receivedBroadcast = false;

	private void Start()
	{
        Initialize();
	}

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!_receivedBroadcast)
        {
            _receivedBroadcast = true;
            base.OnReceivedBroadcast(fromAddress, data);
            Debug.Log("FromAddress: " + fromAddress + "  Data: " + data);
            string[] addressSplit  = fromAddress.Split(':');
            string[] dataSplit = data.Split(':');
            NetworkManager.singleton.networkAddress = addressSplit[addressSplit.Length - 1];
            NetworkManager.singleton.networkPort = int.Parse(dataSplit[dataSplit.Length - 1]);
            NetworkManager.singleton.StartClient();
        }
    }

    public void StartListeningBroadcast() {
        StartAsClient();
    }

    public void StartAsHost () {

        Debug.Log("in startAsHost");
        NetworkManager.singleton.StartHost();
        StartAsServer();
    }

}
