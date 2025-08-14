using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListUI : MonoBehaviour
{
    [SerializeField] private GameObject lobbyItemPrefab;
    [SerializeField] private Transform contentParent;

    private void Start()
    {
        if (MatchManager.instance != null)
        {
            MatchManager.instance.lobbyListChangedEvent += UpdateLobbyList;
            MatchManager.instance.GetLobbyList().Forget();
        }
    }

    private void OnDisable()
    {
        //MatchManager.instance.lobbyListChangedEvent -= UpdateLobbyList;
    }

    private void UpdateLobbyList(List<Lobby> lobbies)
    {
        // 기존 아이템 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 새로운 로비 목록 생성
        foreach (var lobby in lobbies)
        {
            var item = Instantiate(lobbyItemPrefab, contentParent);
            var ui = item.GetComponent<LobbyItemUI>();
            ui.SetLobby(lobby);
        }
    }
}
