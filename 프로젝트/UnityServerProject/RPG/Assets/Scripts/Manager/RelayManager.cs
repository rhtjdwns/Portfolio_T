using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay.Models;
using System;

public class RelayManager : MonoBehaviour
{
    public struct RelayHostData
    {
        public string JoinCode;
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationId;
        public byte[] AllocationIdBytes;
        public byte[] ConnectionData;
        public byte[] Key;
    }

    public struct RelayJoinData
    {
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationId;
        public byte[] AllocationIdBytes;
        public byte[] ConnectionData;
        public byte[] HostConnectionData;
        public byte[] Key;
    }

    // Host
    public static async UniTask<RelayHostData> SetupRelay(int maxConn, string environment)
    {
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);

        // 유니티 서비스 연결
        await UnityServices.InitializeAsync();

        // 로그인이 안되어 있다면 익명으로 시작
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Allocation allocation = await Unity.Services.Relay.RelayService.Instance.CreateAllocationAsync(maxConn);

        RelayHostData data = new RelayHostData
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,

            AllocationId = allocation.AllocationId,
            AllocationIdBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            Key = allocation.Key,
        };

        data.JoinCode = await Unity.Services.Relay.RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        return data;
    }

    // Join
    public static async UniTask<RelayJoinData> JoinRelay(string joinCode, string environment)
    {
        Debug.Log($"Start Join By {joinCode}");

        InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);

        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        JoinAllocation allocation = await Unity.Services.Relay.RelayService.Instance.JoinAllocationAsync(joinCode);

        RelayJoinData data = new RelayJoinData
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,

            AllocationId = allocation.AllocationId,
            AllocationIdBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            Key = allocation.Key,
        };

        return data;
    }
}
