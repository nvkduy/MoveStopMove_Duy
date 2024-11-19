using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform TF;

    private Transform playerTF;

    private void OnEnable()
    {
        LevelManager.Instance.PlayerTF += SetPlayerTF;
        
    }
   

    private void LateUpdate()
    {
        if (playerTF != null)
        {
            TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
        }
    }

    public void SetPlayerTF(Transform newPlayerTF)
    {
        playerTF = newPlayerTF;
    }
}