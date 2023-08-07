using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineVirtualCamera
    {
        get
        {
            if (cinemachineVirtualCamera == null)
            {
                cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            }

            return cinemachineVirtualCamera;
        }
    }

    private CinemachineVirtualCamera cinemachineVirtualCamera;
}
