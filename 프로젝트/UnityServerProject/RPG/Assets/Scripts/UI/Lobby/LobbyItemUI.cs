using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private Button joinButton;

    private Lobby lobby;

    public void SetLobby(Lobby newLobby)
    {
        lobby = newLobby;
        lobbyNameText.text = newLobby.Name;

        joinButton.onClick.RemoveAllListeners();
        joinButton.onClick.AddListener(() =>
        {
            MatchManager.instance.JoinLobbyByUI(lobby).Forget();
        });
    }
}
