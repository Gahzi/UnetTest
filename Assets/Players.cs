using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Players : NetworkBehaviour
{
    public struct PlayerInfo
    {
        public string name;
        public NetworkInstanceId netId;
        public override string ToString()
        {
            return name + " (" + netId + ")";
        }
    };

    public class SyncListPlayers : SyncListStruct<PlayerInfo> { }
    private SyncListPlayers _players = new SyncListPlayers();

    void Start()
    {
        _players.Callback = GameManager.Instance.OnPlayerChanged;
    }

    public List<PlayerInfo> GetPlayers()
    {
        return (_players.ToList());
    }

    public void AddPlayer(GameObject newPlayer)
    {
        if (newPlayer == null)
        {
            Debug.LogError("AddPlayer null");
            return;
        }

        NetworkIdentity playerNetworkIdentity = newPlayer.GetComponent<NetworkIdentity>();
        if (playerNetworkIdentity == null)
        {
            Debug.LogError("AddPlayer no network identity");
            return;
        }

        PlayerInfo newPlayerInfo = new PlayerInfo();
        newPlayerInfo.name = newPlayer.name;
        newPlayerInfo.netId = playerNetworkIdentity.netId;

        _players.Add(newPlayerInfo);
    }

    public void RemovePlayer(GameObject removedPlayer)
    {
        if (removedPlayer == null)
        {
            Debug.LogError("RemovePlayer null");
            return;
        }

        NetworkIdentity removedNetworkIdentity = removedPlayer.GetComponent<NetworkIdentity>();
        if (removedNetworkIdentity == null)
        {
            Debug.LogError("RemovePlayer no network identity");
            return;
        }

        NetworkInstanceId remInstancedId = removedNetworkIdentity.netId;
        for(int i = 0; i < _players.Count; i++)
        {
            PlayerInfo curPlayerInfo = _players[i];
            if (remInstancedId == curPlayerInfo.netId)
            {
                _players.Remove(curPlayerInfo);
                break;
            }
        }
    }
}