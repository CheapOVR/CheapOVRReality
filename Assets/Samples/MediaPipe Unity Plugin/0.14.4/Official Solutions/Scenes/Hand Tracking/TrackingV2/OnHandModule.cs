using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using Mediapipe.Tasks.Components.Containers;
using Mediapipe.Unity.CoordinateSystem;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class OnHandModule : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;
        [SerializeField] public GameObject MiddlePointer; // for menu call button
        [SerializeField] public Canvas menuCaller; // Button for show menu


        // Update is called once per frame
        void Update()
        {
            if (mainTrackingModule.IsListActive)
            {
                if (mainTrackingModule._currentHandLandmarkLists.Count > 1)
                {
                    var middlePoint = Vector3.Lerp(mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[1].Landmark[9]), mainTrackingModule._screen.GetComponent<RectTransform>().rect.GetPoint(mainTrackingModule._currentHandLandmarkLists[1].Landmark[0]), 0.7f);
                    MiddlePointer.gameObject.transform.localPosition = new Vector3(0 - middlePoint.x, middlePoint.y, middlePoint.z);
                    menuCaller.gameObject.transform.position = Vector3.Lerp(MiddlePointer.gameObject.transform.position, mainTrackingModule.PlayerObject.transform.position, 0.65f);
                    menuCaller.gameObject.transform.rotation = mainTrackingModule.PlayerObject.transform.rotation;
                    menuCaller.gameObject.SetActive(true);
                }
                else menuCaller.gameObject.SetActive(false);
            }
            else menuCaller.gameObject.SetActive(false);
        }

    }
}
