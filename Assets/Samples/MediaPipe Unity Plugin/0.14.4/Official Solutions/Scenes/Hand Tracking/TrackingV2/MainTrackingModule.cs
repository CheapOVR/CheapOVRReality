using Mediapipe.Tasks.Components.Containers;
using Mediapipe.Unity.CoordinateSystem;
using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


namespace Mediapipe.Unity.Sample.HandTracking
{
    public class MainTrackingModule : MonoBehaviour
    {
        public IReadOnlyList<NormalizedLandmarkList> _currentHandLandmarkLists; // Main list with all hand landmarks
        public IReadOnlyList<NormalizedRect> _currentRectsLists; // Main list with all rectangles for hands
        [SerializeField] public RawImage _screen; // Var for main screen (Used for coordinates-converting
        [SerializeField] private GameObject _annotationLayer; // Var for layers with hand prefabs
        [SerializeField] private TextMeshProUGUI DebugText; // Debug text panel (Above the screen)

        [SerializeField] public GameObject PlayerObject;

        [SerializeField] public Canvas LastContactedMenu; // I dont know what the fuck is it

        [SerializeField] public Canvas menu; // Menu
        [SerializeField] public GameObject menuParent; // Menu

        [SerializeField] public Canvas CenterPoint; // Menu

        [SerializeField] public GameObject indexPointerA; // Pointer for index finger
        [SerializeField] public GameObject indexPointerB; // Pointer for index finger
        [SerializeField] public GameObject MiddlePointer; // for middle finger
        [SerializeField] public GameObject thumbPoint; // thumb finger

        [SerializeField] public GameObject Cylinder; // Circle in the point when you changing position of panel

        [SerializeField] CommunicatorWithBrowser Communicator;

        [SerializeField] public GameObject BrowserPointer; // thumb finger

        [SerializeField] public KeyboardScript _KeyboardScript;

        public bool ActiveCenterPoint = true; // Is center point used for system 
        public bool ActiveHandTracking = true; // Is hands used for system
        public bool ActiveControllerTracking = false; // Is hands used for system
        public bool IsListActive = false; // Is landmarks list configured to work (and exist)
        public bool ShowHandSkeleton = true;
        public bool UseInteractionWithBrowser = true;

        // Points
        public Vector3 IndexFingerPointA = new Vector3(0, 0, 0);
        public Vector3 HandFingerPoint1 = new Vector3(0, 0, 0);
        public Vector3 ThumbFingerPoint = new Vector3(0, 0, 0);

        public void ShowHandSkeletonMethod(bool _ShowHandSkeleton)
        {
            ShowHandSkeleton = _ShowHandSkeleton;
            if (!_ShowHandSkeleton) PlayerPrefs.SetInt("SkeletonState", 0);
            else if (_ShowHandSkeleton) PlayerPrefs.SetInt("SkeletonState", 1);
        }

        public void HandTracking()
        {
            Debug.Log("Hand tracking is now set.");
            CenterPoint.gameObject.SetActive(false);
            ActiveCenterPoint = false;
            ActiveHandTracking = true;
            ActiveControllerTracking = false;
            PlayerPrefs.SetInt("TrackingType", 0);
        }

        public void CenterTracking()
        {
            Debug.Log("Center tracking is now set.");
            CenterPoint.gameObject.SetActive(true);
            ActiveCenterPoint = true;
            ActiveHandTracking = false;
            ActiveControllerTracking = false;
            PlayerPrefs.SetInt("TrackingType", 1);
        }

        public void ControllerTracking()
        {
            Debug.Log("Controller tracking is now set.");
            CenterPoint.gameObject.SetActive(true);
            ActiveCenterPoint = false;
            ActiveHandTracking = false;
            ActiveControllerTracking = true;
            PlayerPrefs.SetInt("TrackingType", 2);
        }

        public void Open_Menu()
        {
            Debug.Log("Menu opened.");
            menu.gameObject.SetActive(!menu.gameObject.activeSelf);
            menuParent.transform.rotation = PlayerObject.transform.rotation;
        }

        public void Start()
        {
            Debug.Log("Tracking machine started!");
            InvokeRepeating("Update_Every_500ms", 2, 0.5f);
        }

        public void ChangePositionOfWindow()
        {
            if (LastContactedMenu.gameObject.activeSelf && !LastContactedMenu.gameObject.CompareTag("SafePane"))
            {
                Debug.Log("Changing position of panel");
                LastContactedMenu.gameObject.transform.position = Cylinder.transform.position;
                LastContactedMenu.gameObject.transform.rotation = PlayerObject.transform.rotation;
            }
        }

        private void SetCoordinates() // Set all coordinates to their variables
        {
            if (IsListActive)
            {
                HandFingerPoint1 = _screen.GetComponent<RectTransform>().rect.GetPoint(_currentHandLandmarkLists[0].Landmark[5]);
                IndexFingerPointA = _screen.GetComponent<RectTransform>().rect.GetPoint(_currentHandLandmarkLists[0].Landmark[8]);
                ThumbFingerPoint = _screen.GetComponent<RectTransform>().rect.GetPoint(_currentHandLandmarkLists[0].Landmark[4]);
                indexPointerA.transform.localPosition = new Vector3(0 - IndexFingerPointA.x, IndexFingerPointA.y, indexPointerA.transform.localPosition.z);
                thumbPoint.transform.localPosition = new Vector3(0 - ThumbFingerPoint.x, ThumbFingerPoint.y, thumbPoint.transform.localPosition.z);
            }
        }

        private void CheckHandSize() // Check, if hand small (Maybe its a leg?)
        {
            if (IsListActive && ShowHandSkeleton)
            {
                float HandSize = (_currentRectsLists[0].Height * _currentRectsLists[0].Width);
                Debug.Log(HandSize);
                if (HandSize < 0.10)
                {
                    IsListActive = false;
                    _annotationLayer.gameObject.SetActive(false);
                }
                else _annotationLayer.gameObject.SetActive(true);
                _annotationLayer.transform.localPosition = new Vector3(0, 0, -(520+HandSize*50));
            }
            else _annotationLayer.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update_Every_500ms()
        {
            IsListActive = ((_currentHandLandmarkLists != null) && (ActiveHandTracking)); // Check if Landmarks list exists and not null, and hand tracking is active.
            CheckHandSize();
            SetCoordinates();
        }

        public void EmulateClick(Vector3 position) // Do click just by coords on a screen.
        {
            Debug.Log("Click from EmulateClick on " + position);
            Ray ray = new Ray(position, PlayerObject.transform.position - position); // Prepare raycast from point to the camera
            float maxDistance = Vector3.Distance(position, PlayerObject.transform.position); // Calculate distance to camera
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance); // Throw raycast from point to the camera
            //Debug.DrawRay(position, PlayerObject.transform.position - position);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Interface")) // If there interface on a way
                {
                    if (!hit.collider.gameObject.GetComponentInParent<Canvas>().CompareTag("SafePane")) LastContactedMenu = hit.collider.gameObject.GetComponentInParent<Canvas>();
                    ExecuteEvents.Execute(hit.collider.gameObject.GetComponent<Button>().gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler); // Do clicking on a button (If its a button) by emulating mouse event
                    break;
                }
                else if (hit.collider.CompareTag("ScreenCast")) // If there is a raycast...
                {
                    //ClientScript.Finger = ScreenCaster.GetComponent<RectTransform>().rect.GetPoint(_currentHandLandmarkLists[0].Landmark[8]);
                }
                else if (hit.collider.CompareTag("Browser") && UseInteractionWithBrowser) // If there is a raycast...
                {
                    BrowserPointer.transform.position = hit.point;
                    Communicator.DoClickBrowser(new Vector2((360 + BrowserPointer.transform.localPosition.x) / 720, (-BrowserPointer.transform.localPosition.y) / 350));
                    DebugText.text = "" + Communicator.pointBrowser;
                    _KeyboardScript.SetMode("Browser");
                }
                else if (hit.collider.CompareTag("KeyboardInteractive")) // If there is a raycast...
                {
                    if (!hit.collider.gameObject.GetComponentInParent<Canvas>().CompareTag("SafePane")) LastContactedMenu = hit.collider.gameObject.GetComponentInParent<Canvas>();
                    _KeyboardScript.SetMode(hit.collider.gameObject.GetComponent<TextMeshProUGUI>());
                }
                else if (hit.collider.CompareTag("StartThatPane")) // If there is a raycast...
                {
                    if (!hit.collider.gameObject.GetComponentInParent<Canvas>().CompareTag("SafePane")) LastContactedMenu = hit.collider.gameObject.GetComponentInParent<Canvas>();
                    _KeyboardScript.SetMode(hit.collider.gameObject.GetComponent<TextMeshProUGUI>());
                }
            }
        }

        public void updateList(List<NormalizedLandmarkList> list) // Update list with hands landmarks
        {
            _currentHandLandmarkLists = list;
        }
        public void updateListRects(List<NormalizedRect> rectlist) // Update list with hands rects
        {
            _currentRectsLists = rectlist;
        }
    }
}
