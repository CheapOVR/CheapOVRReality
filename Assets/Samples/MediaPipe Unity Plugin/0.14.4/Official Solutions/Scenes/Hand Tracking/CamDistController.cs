using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDistController : MonoBehaviour
{
    [SerializeField] Camera leftCamera;
    [SerializeField] Camera rightCamera;
    [SerializeField] MainTrackingModule mainTrackingModule;
    [SerializeField] GameObject FirstSet;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CameraSet"))
        {
            leftCamera.transform.position = new Vector3(PlayerPrefs.GetFloat("CameraSet"), leftCamera.transform.position.y, leftCamera.transform.position.z);
            rightCamera.transform.position = new Vector3(-PlayerPrefs.GetFloat("CameraSet"), leftCamera.transform.position.y, leftCamera.transform.position.z);
            FirstSet.SetActive(false);
        }
        if (PlayerPrefs.HasKey("TrackingType"))
        {
            switch (PlayerPrefs.GetInt("TrackingType"))
            {
                case 0:
                    mainTrackingModule.HandTracking();
                    break;
                case 1:
                    mainTrackingModule.CenterTracking();
                    break;
                case 2:
                    mainTrackingModule.ControllerTracking();
                    break;
            }
            FirstSet.SetActive(false);
        }
        if (PlayerPrefs.HasKey("SkeletonState"))
        {
            if (PlayerPrefs.GetInt("SkeletonState") == 0) mainTrackingModule.ShowHandSkeletonMethod(false);
            else if (PlayerPrefs.GetInt("SkeletonState") == 1) mainTrackingModule.ShowHandSkeletonMethod(true);
            FirstSet.SetActive(false);
        }
    }

    public void ClearMemory()
    {
        PlayerPrefs.DeleteAll();
    }


    public void set025()
    {
        leftCamera.transform.position = new Vector3(0.1f, leftCamera.transform.position.y, leftCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(-0.1f, rightCamera.transform.position.y, rightCamera.transform.position.z);
        PlayerPrefs.SetFloat("CameraSet", 0.1f);
    }
    public void set05()
    {
        leftCamera.transform.position = new Vector3(0.25f, leftCamera.transform.position.y, leftCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(-0.25f, rightCamera.transform.position.y, rightCamera.transform.position.z);
        PlayerPrefs.SetFloat("CameraSet", 0.25f);
    }
    public void set1()
    {
        leftCamera.transform.position = new Vector3(0.5f, leftCamera.transform.position.y, leftCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(-0.5f, rightCamera.transform.position.y, rightCamera.transform.position.z);
        PlayerPrefs.SetFloat("CameraSet", 0.5f);
    }
    public void set2()
    {
        leftCamera.transform.position = new Vector3(1f, leftCamera.transform.position.y, leftCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(-1f, rightCamera.transform.position.y, rightCamera.transform.position.z);
        PlayerPrefs.SetFloat("CameraSet", 1f);
    }
}
