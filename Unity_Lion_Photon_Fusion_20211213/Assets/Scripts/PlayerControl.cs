using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

/// <summary>
/// �Z�J���a���
/// �e�ᥪ�k����
/// �����P�o�g�l�u
/// </summary>
public class PlayerControl : NetworkBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 100)]
    public float speed = 7.5f;
    [Header("�o�g�l�u���j"), Range(0, 1.5f)]
    public float intervalFire = 0.35f;
    [Header("�l�u����")]
    public GameObject bullet;

    /// <summary>
    /// �s�u���ⱱ�
    /// </summary>
    private NetworkCharacterController ncc;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        ncc = GetComponent<NetworkCharacterController>();
    }
    #endregion

    #region ��k
    /// <summary>
    /// Fusion �T�w��s�ƥ� ������ Unity Fixed Update
    /// </summary>
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        Move();
    }
    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        //�p�G �� ��J���
        if(GetInput(out NetworkInputData dataInput))
        {
            //�s�u���ⱱ�.����(�t�� * ��V * �s�u�@���ɶ�)
            ncc.Move(speed * dataInput.direction * Runner.DeltaTime);
        }
    }
    #endregion
}
