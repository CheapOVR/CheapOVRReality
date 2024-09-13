using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
public class AppController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
