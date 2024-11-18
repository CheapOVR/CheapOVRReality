using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.XR.Cardboard;
using Mediapipe.Unity;
using Mediapipe.Unity.Sample.HandTracking;
using static UnityEngine.ParticleSystem;

public class AppController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MainTrackingModule mainmodule;
    [SerializeField] GameObject AnnotationLayer;
    [SerializeField] GameObject PointList;
    [SerializeField] Camera LeftCamera;
    [SerializeField] Camera RightCamera;

    void Start()
    {
        Application.targetFrameRate = 50;
        InvokeRepeating("Update_Position_Of_Layer", 2, 0f);
    }
    void Update_Position_Of_Layer()
    {
        AnnotationLayer.transform.localPosition = new Vector3(0, 0, -540);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void Awake()
    {
        UnityEngine.Screen.sleepTimeout = SleepTimeout.NeverSleep; // Set the parameter to turn off screen timer (turn off time-sleeping)
    }
    // Update is called once per frame
    void Update()
    {
        if (Api.IsGearButtonPressed)
        {
            Api.ScanDeviceParams();
        }
        if (Api.IsCloseButtonPressed)
        {
            UnityEngine.Application.Quit();
        }
    }
}
