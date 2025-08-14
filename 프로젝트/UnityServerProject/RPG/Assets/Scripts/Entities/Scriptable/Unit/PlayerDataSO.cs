using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "RPG/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [System.Serializable]
    public class PlayerInfo
    {
        public string PlayerId;
        public string PlayerName;
        public PlayerController Controller;
        public APIClient.UserInfo UserInfo;

        public PlayerInfo(string id, string name, PlayerController controller, APIClient.UserInfo userInfo)
        {
            PlayerId = id;
            PlayerName = name;
            Controller = controller;
            UserInfo = userInfo;
        }

        public PlayerInfo(string id, string name, PlayerController controller)
        {
            PlayerId = id;
            PlayerName = name;
            Controller = controller;
        }
    }

    public List<PlayerInfo> Players = new List<PlayerInfo>();

    public void ClearPlayers()
    {
        Players.Clear();
    }

    public void AddPlayer(string playerId, string playerName, PlayerController controller)
    {
        if (!Players.Exists(p => p.PlayerId == playerId))
        {
            Players.Add(new PlayerInfo(playerId, playerName, controller));
        }
    }

    public void AddPlayer(string playerId, string playerName, PlayerController controller, APIClient.UserInfo userInfo)
    {
        if (!Players.Exists(p => p.PlayerId == playerId))
        {
            Players.Add(new PlayerInfo(playerId, playerName, controller, userInfo));
        }
    }
} 