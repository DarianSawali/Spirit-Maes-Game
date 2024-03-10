using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CamFollow camFollow;

    private void Awake()
    {
        camFollow = GetComponentInChildren<CamFollow>();
    }

    public void SetCameraTarget(Transform target)
    {
        if (camFollow != null)
        {
            camFollow.SetTarget(target);
        }
        else
        {
            Debug.LogError("CamFollow script not found on the camera's children.");
        }
    }
}

