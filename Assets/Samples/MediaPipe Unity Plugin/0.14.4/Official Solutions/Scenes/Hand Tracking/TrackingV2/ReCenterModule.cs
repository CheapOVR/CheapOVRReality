using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using Mediapipe.Tasks.Components.Containers;
using Mediapipe.Unity.CoordinateSystem;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class ReCenterModule : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;
        [SerializeField] public GameObject MiddlePointer; // for menu call button
        [SerializeField] public GameObject indexPointer; // for menu call button
        [SerializeField] public GameObject thumbPoint; // for menu call button
        [SerializeField] public Canvas menuCaller; // Button for show menu
        [SerializeField] public GameObject Cylinder; // Circle in the point when you changing position of panel

        public Vector3 MiddlePoint = new Vector3(0, 0, 0);
        public Vector3 IndexFingerPoint = new Vector3(0, 0, 0);
        public Vector3 ThumbFingerPoint = new Vector3(0, 0, 0);

        private int Accurancy = 3;


        // Update is called once per frame

        private void Start()
        {
            if (PlayerPrefs.HasKey("ReCenterAccurancy")) Accurancy = PlayerPrefs.GetInt("ReCenterAccurancy");
        }

        public void SetAccurancy(int _Accurancy)
        {
            Accurancy = _Accurancy;
            PlayerPrefs.SetInt("ReCenterAccurancy", Accurancy);
        }
        void Update()
        {
            if (mainTrackingModule.IsListActive)
            {
                IndexFingerPoint = mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[0].Landmark[8]);
                ThumbFingerPoint = mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[0].Landmark[4]);

                if (Vector3.Distance(IndexFingerPoint, ThumbFingerPoint) <= 250 && Vector3.Distance(IndexFingerPoint, mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[0].Landmark[0])) >= Accurancy*100+50)
                {
                    MiddlePoint = Vector3.Lerp(mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[0].Landmark[6]), mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[0].Landmark[3]), 0.5f);

                    var end = Vector3.Lerp(IndexFingerPoint, ThumbFingerPoint, 0.5f);
                    var NewPos = (end + (end - MiddlePoint).normalized * 250);
                    Cylinder.transform.localPosition = new Vector3(0- NewPos.x, NewPos.y, 0);
                    Cylinder.transform.position = Vector3.Lerp(Cylinder.transform.position, mainTrackingModule.PlayerObject.transform.position, 0.5f);
                    Cylinder.SetActive(true);
                }
                else Cylinder.SetActive(false);
            }
            else Cylinder.SetActive(false);
        }

    }
}
