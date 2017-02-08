using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    private static GameManager _instance;

    public List<Player> Players = new List<Player>(4);

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (_instance == null)
                {
                    Debug.Log(typeof(GameManager) +
                        " instance is missing.");
                }
            }

            return _instance;
        }
    }

    

    

    // Use this for initialization
    void Start () {
		
	}

    public override void OnStartClient()
    {
        base.OnStartClient();
        ResetPlayerList();
    }

    // Update is called once per frame
	void Update () {

		
	}

    void FixedUpdate()
    {
        UpdatePlayerList();
    }

    //public void AddPlayer(Player newPlayer)
    //{
    //    bool foundPlayer = false;

    //    for (int i = 0; i < _players.Count; i++)
    //    {
    //        if (_players[i].netIdValue == newPlayer.netId.Value)
    //        {
    //            foundPlayer = true;
    //        }
    //    }

    //    if (!foundPlayer)
    //    {
    //        PlayerInfo newPlayerInfo;
    //        newPlayerInfo.netIdValue = newPlayer.netId.Value;
    //        newPlayer.name = "Player " + (_players.Count + 1);
    //        newPlayerInfo.GO = newPlayer.gameObject;
    //        _players.Add(newPlayerInfo);
    //    }
    //}

    [ClientCallback]
    public void UpdatePlayerList()
    {
        UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        List<Players.PlayerInfo> nmPlayers = nm.GetPlayers();

        if (Players.Count != nmPlayers.Count)
        {
            ResetPlayerList();
        }
    }

    [ClientCallback]
    public void ResetPlayerList()
    {
        Players.Clear();
        UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        List<Players.PlayerInfo> nmPlayers = nm.GetPlayers();
        for (int i = 0; i < nmPlayers.Count; i++)
        {
            GameObject playerGO = ClientScene.FindLocalObject(nmPlayers[i].netId);

            if (playerGO)
            {
                Players.Add(playerGO.GetComponent<Player>());
            }
        }
    }

    public void OnPlayerChanged(SyncList<Players.PlayerInfo>.Operation op, int index)
    {
        UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        List<Players.PlayerInfo> nmPlayers = nm.GetPlayers();
        switch (op)
        {
            case SyncList<Players.PlayerInfo>.Operation.OP_ADD:
                {
                    GameObject addedGO = ClientScene.FindLocalObject(nmPlayers[index].netId);

                    if (addedGO)
                    {
                        Players.Add(addedGO.GetComponent<Player>());
                    }
                    break;
                }

            case SyncList<Players.PlayerInfo>.Operation.OP_CLEAR:
                {
                    Players.Clear();
                    break;
                }

            case SyncList<Players.PlayerInfo>.Operation.OP_INSERT:
                {
                    GameObject addedGO = ClientScene.FindLocalObject(nmPlayers[index].netId);

                    if (addedGO)
                    {
                        Players.Insert(index, addedGO.GetComponent<Player>());
                    }
                    break;
                }

            case SyncList<Players.PlayerInfo>.Operation.OP_REMOVE:
                {
                    GameObject addedGO = ClientScene.FindLocalObject(nmPlayers[index].netId);

                    if (addedGO)
                    {
                        Players.Remove(addedGO.GetComponent<Player>());
                    }
                    break;
                }

            case SyncList<Players.PlayerInfo>.Operation.OP_REMOVEAT:
                {
                    GameObject addedGO = ClientScene.FindLocalObject(nmPlayers[index].netId);

                    if (addedGO)
                    {
                        Players.RemoveAt(index);
                    }
                    break;
                }
        }
    }
}
