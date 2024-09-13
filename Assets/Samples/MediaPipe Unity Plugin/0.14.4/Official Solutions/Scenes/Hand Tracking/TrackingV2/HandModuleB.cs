using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class HandModuleB : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;

        private Vector3 IndexFingerPoint = new Vector3(0, 0, 0);
        private Vector3 IndexFingerPointOld = new Vector3(0, 0, 0);
        private int WasOnPointIndex = 0;

        private int Accurancy = 3;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Update_Every_300ms", 3, 0.3f);
            if (PlayerPrefs.HasKey("HandBAccurancy")) Accurancy = PlayerPrefs.GetInt("HandBAccurancy");
        }

        public void SetAccurancy(int _Accurancy)
        {
            Accurancy = _Accurancy;
            PlayerPrefs.SetInt("HandBAccurancy", Accurancy);
        }

        void IndexClickCheck()
        {
            IndexFingerPoint = mainTrackingModule.indexPointerB.transform.position;
            if (Vector3.Distance(IndexFingerPoint, IndexFingerPointOld) < 30)
            {
                if (WasOnPointIndex >= Accurancy)
                {
                    WasOnPointIndex = 0;
                    mainTrackingModule.EmulateClick(IndexFingerPoint);
                }
                else WasOnPointIndex += 1;
            }
            else WasOnPointIndex = 0;
            IndexFingerPointOld = IndexFingerPoint;
        }

        // Update is called once per frame
        void Update_Every_300ms()
        {
            if (mainTrackingModule.IsListActive)
            {
                IndexClickCheck();
            }
        }

    }
}
