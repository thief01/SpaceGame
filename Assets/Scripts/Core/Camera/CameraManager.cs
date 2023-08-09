using Cinemachine;
using UnityEngine;

namespace Core.Camera
{
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
}
