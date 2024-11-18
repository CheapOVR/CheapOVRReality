using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class CenterPointModule : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;
        [SerializeField] GameObject centerPoint;

        private Vector3 CenterPoint = new Vector3(0, 0, 0);
        private Vector3 CenterPointOld = new Vector3(0, 0, 0);
        private int WasOnPoint = 0;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Update_Every_400ms", 3, 0.4f);
        }

        // Update is called once per frame
        void Update_Every_400ms()
        {
            CenterPoint = centerPoint.transform.position;
            Ray ray = new Ray(CenterPoint, mainTrackingModule.PlayerObject.transform.position - CenterPoint); // Prepare raycast from point to the camera
            float maxDistance = Vector3.Distance(CenterPoint, mainTrackingModule.PlayerObject.transform.position); // Calculate distance to camera
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance); // Throw raycast from point to the camera
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("StartThatPane")) // If there is a raycast...
                {
                    if (!hit.collider.GetComponentInChildren<Canvas>(true).gameObject.activeSelf) hit.collider.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
                }
            }
            if (mainTrackingModule.ActiveCenterPoint)
            {
                if (Vector3.Distance(CenterPoint, CenterPointOld) < 50)
                {
                    if (WasOnPoint >=3)
                    {
                        WasOnPoint = 0;
                        mainTrackingModule.EmulateClick(CenterPoint);
                    }
                    else WasOnPoint += 1;
                }
                else WasOnPoint = 0;
                CenterPointOld = CenterPoint;
            }
        }

    }
}
