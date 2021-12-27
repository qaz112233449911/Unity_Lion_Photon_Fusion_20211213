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
    [Header("������r")]
    public Text textVersion;
    [Header("���a�ͦ���m")]
    public Transform[] traSpawnPoints;

    /// <summary>
    /// ���a��J���ж��W��
    /// </summary>
    private string roomNameInput;
    /// <summary>
    /// �s�u���澹
    /// </summary>
    private NetworkRunner runner;
    private string stringVesion = "LiangWei Copyright 2022. | Version ";
    #endregion

    #region �ƥ�
    private void Awake()
    {
        textVersion.text = stringVesion + Application.version;
    }
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

    /// <summary>
    /// ���a�s�u��J�欰
    /// </summary>
    /// <param name="runner">�s�u���澹</param>
    /// <param name="input">��J��T</param>
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData inputData = new NetworkInputData();                            //�s�W �s�u��J��� ���c

        #region �ۭq��J����P���ʸ�T
        if (Input.GetKey(KeyCode.W)) inputData.direction += Vector3.forward;            //W �e
        if (Input.GetKey(KeyCode.S)) inputData.direction += Vector3.back;               //S ��
        if (Input.GetKey(KeyCode.A)) inputData.direction += Vector3.left;               //A ��
        if (Input.GetKey(KeyCode.D)) inputData.direction += Vector3.right;              //D �k

        inputData.inputFire = Input.GetKey(KeyCode.Mouse0);                             //���� �o�g
        #endregion

        #region �ƹ��y�гB�z
        inputData.positionMouse = Input.mousePosition;                                      //���o �ƹ��y��
        inputData.positionMouse.z = 60;                                                     //�]�w �ƹ��y�� Z �b - �i�H����3D����A�j����v����Y

        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(inputData.positionMouse);     //�z�L API �N�ƹ��ର�@�ɮy��
        inputData.positionMouse = mouseToWorld;                                             //�x�s�ഫ�᪺�ƹ��y��
        #endregion

        input.Set(inputData);                                                           //��J��T.�]�w(�s�u��J���)
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    /// <summary>
    /// ���a��ƶ��X : ���a�ѦҸ�T�B���a�s�u����
    /// </summary>
    private Dictionary<PlayerRef, NetworkObject> players = new Dictionary<PlayerRef, NetworkObject>();

    /// <summary>
    /// ���a���\�[�J�ж���
    /// </summary>
    /// <param name="runner">�s�u���澹</param>
    /// <param name="player">���a��T</param>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //�H���ͦ��I = Unity ���H���d��(0�A�ͦ���m�ƶq)
        int randomSpawnPoint = UnityEngine.Random.Range(0, traSpawnPoints.Length);
        //�s�u���澹.�ͦ�(����A�y�СA���סA���a��T)
        NetworkObject playerNetworkObject = runner.Spawn(goPlayer, traSpawnPoints[randomSpawnPoint].position, Quaternion.identity, player);
        //�N���a�ѦҸ�T�P���a�s�u����K�[��r�嶰�X��
        players.Add(player, playerNetworkObject);
    }

    /// <summary>
    /// ���a���}�ж���
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="player"></param>
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //�p�G ���}�����a�s�u���� �s�b �N�R��
        if (players.TryGetValue(player,out NetworkObject playerNetworkObject))
        {
            runner.Despawn(playerNetworkObject);            //�s�u���澹.�����ͦ�(�Ӫ��a�s�u���󲾰�)
            players.Remove(player);                         //���a���X.����(�Ӫ��a)
        }
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
