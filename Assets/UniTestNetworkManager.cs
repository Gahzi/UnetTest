using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;

public class UniTestNetworkManager : NetworkManager
{
    [SerializeField]
    private Players _players;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Players.PlayerInfo> GetPlayers()
    {
        return _players.GetPlayers();
    }

    // called when a new player is added for a client
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        GameObject playerGO = null;
        for (int i = 0; i < conn.playerControllers.Count; i++)
        {
            PlayerController playerController = conn.playerControllers[i];

            if (playerController.playerControllerId == playerControllerId)
            {
                playerGO = playerController.gameObject;
            }
        }

        if (playerGO)
        {
            _players.AddPlayer(playerGO);
        }
        

        //GameManager gm = GameManager.Instance;
        //if (gm && playerGO)
        //{
        //    Player player = playerGO.GetComponent<Player>();
        //    gm.AddPlayer(player);
        //}
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        for (int i = 0; i < conn.playerControllers.Count; i++)
        {
            PlayerController curPlayerController = conn.playerControllers[i];
            _players.RemovePlayer(curPlayerController.gameObject);

            //for (int j = 0; j < _players.Count; j++)
            //{
            //    PlayerInfo curPlayerInfo = _players[j];
            //    if (curPlayerNetId == curPlayerInfo.netId)
            //    {
            //        _players.Remove(curPlayerInfo);
            //    }
            //}
        }

        base.OnServerRemovePlayer(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        for (int i = 0; i < conn.playerControllers.Count; i++)
        {
            PlayerController curPlayerController = conn.playerControllers[i];
            _players.RemovePlayer(curPlayerController.gameObject);

            //NetworkInstanceId curPlayerNetId = curPlayerController.gameObject.GetComponent<NetworkIdentity>().netId;

            //for (int j = 0; j < _players.Count; j++)
            //{
            //    PlayerInfo curPlayerInfo = _players[j];
            //    if (curPlayerNetId == curPlayerInfo.netId)
            //    {
            //        _players.Remove(curPlayerInfo);
            //    }
            //}
        }

        base.OnServerDisconnect(conn);
    }

    public void OnPlayerChanged(SyncList<Players.PlayerInfo>.Operation op, int index)
    {
        //switch (op)
        //{
        //    case SyncList<UniTestNetworkManager.PlayerInfo>.Operation.OP_ADD:
        //        {
        //            UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        //            GameObject addedGO = ClientScene.FindLocalObject(nm._players[index].netId);

        //            if (addedGO)
        //            {
        //                Players.Add(addedGO.GetComponent<Player>());
        //            }
        //            break;
        //        }

        //    case SyncList<UniTestNetworkManager.PlayerInfo>.Operation.OP_CLEAR:
        //        {
        //            Players.Clear();
        //            break;
        //        }

        //    case SyncList<UniTestNetworkManager.PlayerInfo>.Operation.OP_INSERT:
        //        {
        //            UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        //            GameObject addedGO = ClientScene.FindLocalObject(nm._players[index].netId);

        //            if (addedGO)
        //            {
        //                Players.Insert(index, addedGO.GetComponent<Player>());
        //            }
        //            break;
        //        }

        //    case SyncList<UniTestNetworkManager.PlayerInfo>.Operation.OP_REMOVE:
        //        {
        //            UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        //            GameObject addedGO = ClientScene.FindLocalObject(nm._players[index].netId);

        //            if (addedGO)
        //            {
        //                Players.Remove(addedGO.GetComponent<Player>());
        //            }
        //            break;
        //        }

        //    case SyncList<UniTestNetworkManager.PlayerInfo>.Operation.OP_REMOVEAT:
        //        {
        //            UniTestNetworkManager nm = UniTestNetworkManager.singleton.gameObject.GetComponent<UniTestNetworkManager>();
        //            GameObject addedGO = ClientScene.FindLocalObject(nm._players[index].netId);

        //            if (addedGO)
        //            {
        //                Players.RemoveAt(index);
        //            }
        //            break;
        //        }
        //}
    }
}
