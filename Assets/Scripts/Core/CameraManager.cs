using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CinemachineVirtualCamera CinemachineVirtualCamera { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }


}
