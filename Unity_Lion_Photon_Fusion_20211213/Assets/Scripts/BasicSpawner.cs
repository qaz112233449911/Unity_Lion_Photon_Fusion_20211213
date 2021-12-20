using UnityEngine;
using UnityEngine.UI;
using Fusion;                   //�ޥ� Fusion �R�W�Ŷ�
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

//INetworkRunnerCallbacks �s�u���澹�^�I�����ARunner ���澹�B�z�欰��|�^�I����������k
/// <summary>
/// �s�u�򩳲��;�
/// </summary>
public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    #region ���
    [Header("�ЫػP�[�J�ж����")]
    public InputField inputFiledCreateRoom;
    public InputField inputFiledJoinRoom;
    [Header("���a����� - �s�u�w�s��")]
    public NetworkPrefabRef goPlayer;
    [Header("�e���s�u")]
    public GameObject goCanvas;

    /// <summary>
    /// ���a��J���ж��W��
    /// </summary>
    private string roomNameInput;
    /// <summary>
    /// �s�u���澹
    /// </summary>
    private NetworkRunner runner;
    #endregion

    #region ��k
    /// <summary>
    /// ���s�I�Y�I�s : �Ыةж�
    /// </summary>
    public void BtnCreateRoom()
    {
        roomNameInput = inputFiledCreateRoom.text;
        print("�Ыةж� : " + roomNameInput);
        StartGame(GameMode.Host);
    }

    /// <summary>
    /// ���s�I�Y�I�s : �[�J�ж�
    /// </summary>
    public void BtnJoinRoom()
    {
        roomNameInput = inputFiledJoinRoom.text;
        print("�[�J�ж� : " + roomNameInput);
        StartGame(GameMode.Client);
    }

    //async �D�P�B�B�z : ����t�ήɳB�z�s�u
    /// <summary>
    /// �}�l�s�u�C��
    /// </summary>
    /// <param name="mode">�s�u�Ҧ� : �D���B�Ȥ�</param>
    private async void StartGame(GameMode mode)
    {
        print("<color=yellow>�}�l�s�u</color>");

        runner = gameObject.AddComponent<NetworkRunner>();      //�s�u���澹 = �K�[����<�s�u���澹>
        runner.ProvideInput = true;                             //�s�u���澹.�O�_���ѿ�J = �O

        //���ݳs�u : �C���s�u�{���B�ж��W�١B�s�u�᪺�����B�����޲z��
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = roomNameInput,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneObjectProvider = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        print("<color=yellow>�s�u����</color>");
        goCanvas.SetActive(false);
    }
    #endregion

    #region Fusion �^�I�禡�ϰ�
    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    /// <summary>
    /// ���a���\�[�J�ж���
    /// </summary>
    /// <param name="runner">�s�u���澹</param>
    /// <param name="player">���a��T</param>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        runner.Spawn(goPlayer, new Vector3(-5, 1, -10), Quaternion.identity, player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
    #endregion
}
