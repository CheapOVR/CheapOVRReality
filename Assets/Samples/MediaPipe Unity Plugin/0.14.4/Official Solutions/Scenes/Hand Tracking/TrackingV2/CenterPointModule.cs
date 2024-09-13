using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (mainTrackingModule.ActiveCenterPoint)
            {
                CenterPoint = centerPoint.transform.position;
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
