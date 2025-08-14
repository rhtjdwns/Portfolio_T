using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private Transform playerListParent;
    [SerializeField] private TMP_Text playerSlotPrefab;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(() => MatchManager.instance.LeaveLobby().Forget());

        if (MatchManager.instance != null)
        {
            MatchManager.instance.joinLobbyEvent += UpdatePlayerList;
            MatchManager.instance.leaveLobbyEvent += ClearPlayerList;
            MatchManager.instance.kickedFromLobbyEvent += ClearPlayerList;
        }
    }

    public void UpdatePlayerList(Lobby lobby)
    {
        // 기존 슬롯 제거
        foreach (Transform child in playerListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in lobby.Players)
        {
            var slot = Instantiate(playerSlotPrefab, playerListParent);
            var name = player.Data.ContainsKey(NetworkConstants.PLAYERNAME_KEY) ?
                       player.Data[NetworkConstants.PLAYERNAME_KEY].Value : "Unknown";

            slot.text = name;
        }
    }

    private void OnDestroy()
    {
        if (MatchManager.instance != null)
        {
            MatchManager.instance.joinLobbyEvent -= UpdatePlayerList;
            MatchManager.instance.leaveLobbyEvent -= ClearPlayerList;
            MatchManager.instance.kickedFromLobbyEvent -= ClearPlayerList;
        }        
    }

    private void ClearPlayerList()
    {
        foreach (Transform child in playerListParent)
        {
            Destroy(child.gameObject);
        }
    }
}
