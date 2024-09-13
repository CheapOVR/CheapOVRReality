using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class ControllerTrackingModule : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;
        [SerializeField] GameObject centerPoint;
        [SerializeField] GameObject centerPointCanvas;

        public float horizontalSpeed = 4.0f;
        public float verticalSpeed = 4.0f;

        private float h, v;

        private bool DoUpdateTimer = false;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Update_Every_1s", 2, 1f);
        }

        void Update_Every_1s()
        {
            if (!DoUpdateTimer)
            {
                DoUpdateTimer = true;
                Debug.Log("Clicking timer was updated");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mainTrackingModule.ActiveControllerTracking)
            {
                h = horizontalSpeed * Input.GetAxis("Vertical");
                v = verticalSpeed * Input.GetAxis("Horizontal");
                h += horizontalSpeed * Input.GetAxis("Mouse Y");
                v += verticalSpeed * Input.GetAxis("Mouse X");
                if (centerPoint.transform.localPosition.x + v < 1200 && centerPoint.transform.localPosition.x + v > -1200 && centerPoint.transform.localPosition.y + h < 1200 && centerPoint.transform.localPosition.y + h > -1200) {
                    centerPoint.transform.localPosition = new Vector3(centerPoint.transform.localPosition.x + v, centerPoint.transform.localPosition.y + h, centerPoint.transform.localPosition.z);
                }

                centerPointCanvas.transform.localPosition = new Vector3(centerPointCanvas.transform.localPosition.x + v, centerPointCanvas.transform.localPosition.y + h, centerPointCanvas.transform.localPosition.z);

                if (Input.GetAxis("Submit") > 0 || Input.GetAxis("Cancel") > 0 || Input.GetAxis("Fire1") > 0 || Input.GetAxis("Jump") > 0 || Input.GetAxis("Fire2") > 0 || Input.GetAxis("Fire3") > 0)
                {
                    if (DoUpdateTimer) {
                        mainTrackingModule.EmulateClick(centerPoint.transform.position);
                        DoUpdateTimer = false; 
                    }
                }
            }
        }

    }
}
