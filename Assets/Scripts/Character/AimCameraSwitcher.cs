using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera hipFireCamera;
    [SerializeField] private CinemachineVirtualCamera shoulderAimCamera;


    private void Start()
    {
        
    }



    public void SwitchToShoulderAim()
    {
        hipFireCamera.Priority = 10;
        shoulderAimCamera.Priority = 20;
    }


    public void SwitchToHipFire()
    {
        hipFireCamera.Priority= 20;
        shoulderAimCamera.Priority= 10;
    }
}
