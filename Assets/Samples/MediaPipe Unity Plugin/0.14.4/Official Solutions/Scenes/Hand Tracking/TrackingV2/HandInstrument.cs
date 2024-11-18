using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Experimental.GlobalIllumination;
using Mediapipe.Unity.Sample.HandTracking;

public class HandInstrument : MonoBehaviour
{
    [SerializeField] MultiHandLandmarkListAnnotation _MultiHandLandmarkListAnnotation;
    HandLandmarkListAnnotation _HandLandmarkListAnnotation;
    PointListAnnotation PointAnnotations;
    [SerializeField] Canvas MenuOpenCanvas;

    [SerializeField] MainTrackingModule _MainTrackingModule;
    private bool done = false;

    private int FingerNumber = 8;

    public PointAnnotation GetAnnotation(int PointNumber, int HandNumber)
    {
        return _MultiHandLandmarkListAnnotation.gameObject.GetComponentsInChildren<HandLandmarkListAnnotation>(true)[HandNumber]
            .gameObject.GetComponentInChildren<PointListAnnotation>(true)[PointNumber];
    }

    public void ReCreateInteractor(int _FingerNumber = 8)
    {
        FingerNumber= _FingerNumber;
        done = false;
    }
    private void Start()
    {
    }

    PointAnnotation Point0;

    public void ChangePositionOfButton()
    {
        MenuOpenCanvas.transform.position = Point0.transform.position;
        MenuOpenCanvas.transform.rotation = _MainTrackingModule.PlayerObject.transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!done)
        {
            try
            {
                _HandLandmarkListAnnotation = _MultiHandLandmarkListAnnotation.gameObject.GetComponentsInChildren<HandLandmarkListAnnotation>(true)[0];
                PointAnnotations = _HandLandmarkListAnnotation.gameObject.GetComponentInChildren<PointListAnnotation>(true);
                PointAnnotations[FingerNumber].gameObject.AddComponent<Interactor>()._MainTrackingModule = _MainTrackingModule;

                GetAnnotation(4, 0).gameObject.tag = "ThumbFinger";
                Point0 = GetAnnotation(0, 1);
                InvokeRepeating("ChangePositionOfButton", 0, 0.01f);
                MenuOpenCanvas.gameObject.SetActive(true);
                done = true;
            }
            catch
            {
                done = false;
            }
        }
    }
}
