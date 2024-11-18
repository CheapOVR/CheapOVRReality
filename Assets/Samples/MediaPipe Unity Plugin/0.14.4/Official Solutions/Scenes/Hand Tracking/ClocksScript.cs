using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Mediapipe.Unity.Sample.HandTracking
{
    public class ClocksScript : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ClocksText;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ClocksText.text = DateTime.Now.ToLongTimeString();
        }
    }
}
