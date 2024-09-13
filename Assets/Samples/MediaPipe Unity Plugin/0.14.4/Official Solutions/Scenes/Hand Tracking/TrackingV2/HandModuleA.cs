using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class HandModuleA : MonoBehaviour
    {
        [SerializeField] MainTrackingModule mainTrackingModule;

        private Vector3 IndexFingerPoint = new Vector3(0, 0, 0);
        private Vector3 IndexFingerPointOld = new Vector3(0, 0, 0);
        private int WasOnPointIndex = 0;

        private Vector3 ThumbFingerPoint = new Vector3(0, 0, 0);
        private int WasOnPointThumb = 0;

        private int Accurancy = 3;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Update_Every_300ms", 2, 0.5f);
            if (PlayerPrefs.HasKey("HandAAccurancy")) Accurancy = PlayerPrefs.GetInt("HandAAccurancy");
        }

        public void SetAccurancy(int _Accurancy)
        {
            Accurancy = _Accurancy;
            PlayerPrefs.SetInt("HandAAccurancy", Accurancy);
        }

        void IndexClickCheck()
        {
            IndexFingerPoint = mainTrackingModule.indexPointerA.transform.position;
            if (Vector3.Distance(IndexFingerPoint, IndexFingerPointOld) < 70 && Vector3.Distance(mainTrackingModule.HandFingerPoint1, mainTrackingModule.IndexFingerPointA) > 150)
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

        void ThumbClickCheck()
        {
            ThumbFingerPoint = mainTrackingModule.thumbPoint.transform.position;
            if (Vector3.Distance(ThumbFingerPoint, IndexFingerPoint) < 15)
            {
                if (WasOnPointThumb >= Accurancy)
                {
                    WasOnPointThumb = 0;
                    mainTrackingModule.ChangePositionOfWindow();
                }
                else WasOnPointThumb += 1;
            }
            else WasOnPointThumb = 0;
        }

        // Update is called once per frame
        void Update_Every_300ms()
        {
            if (mainTrackingModule.IsListActive)
            {
                IndexClickCheck();
                ThumbClickCheck();
            }
        }

    }
}
