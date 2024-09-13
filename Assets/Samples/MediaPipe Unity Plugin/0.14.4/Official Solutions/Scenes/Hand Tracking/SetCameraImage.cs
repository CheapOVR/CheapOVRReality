using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SetCameraImage : MonoBehaviour
{
    [SerializeField] public RawImage CameraImage;
    [SerializeField] public RawImage CanvasImage;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        CanvasImage.texture = CameraImage.texture;
    }
}
