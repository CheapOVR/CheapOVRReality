using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppsController : MonoBehaviour
{
    [SerializeField] GameObject Browser;
    [SerializeField] Button SearchButton;
    [SerializeField] TextMeshProUGUI URL;

    [SerializeField] GameObject LeftScreen;
    [SerializeField] GameObject RightScreen;

    [SerializeField] GameObject InfoPanel;
    [SerializeField] GameObject ScreenCast;
    [SerializeField] GameObject Calculator;
    [SerializeField] GameObject ObjectsPoint;
    [SerializeField] GameObject VREnvironments;
    [SerializeField] GameObject AppsList;
    [SerializeField] GameObject Note;

    [SerializeField] GameObject Keyboard;
    [SerializeField] GameObject KeyboardPoint;

    [SerializeField] GameObject Menu;

    [SerializeField] MainTrackingModule TrackingModule;
    [SerializeField] KeyboardScript _KeyboardScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void TurnObject(GameObject _object)
    {
        _object.transform.position = ObjectsPoint.transform.position;
        _object.transform.rotation = ObjectsPoint.transform.rotation;
        _object.SetActive(!_object.activeSelf);
        TrackingModule.LastContactedMenu = _object.gameObject.GetComponent<Canvas>();
    }

    public void TurnKeyboard()
    {
        Keyboard.transform.position = KeyboardPoint.transform.position;
        Keyboard.transform.rotation = KeyboardPoint.transform.rotation;
        Keyboard.SetActive(!Keyboard.activeSelf);
    }

    public void TurnKeyboard(bool active)
    {
        Keyboard.transform.position = KeyboardPoint.transform.position;
        Keyboard.transform.rotation = KeyboardPoint.transform.rotation;
        Keyboard.SetActive(active);
    }

    public void TurnBrowser()
    {
        TurnObject(Browser);
        TurnKeyboard(Browser.activeSelf);
        _KeyboardScript.SetMode("Browser");
    }

    public void TurnBrowser(string _URL)
    {
        URL.text = _URL;
        SearchButton.onClick.Invoke();
        TurnBrowser();
    }

    public void TurnSettings()
    {
        TurnObject(InfoPanel);
    }

    public void TurnScreenCast()
    {
        TurnObject(ScreenCast);
    }
    public void TurnCalculator()
    {
        TurnObject(Calculator);
    }

    public void TurnAppsList()
    {
        TurnObject(AppsList);
    }

    public void TurnNote()
    {
        TurnObject(AppsList);
        TurnObject(Note);
        TurnKeyboard();
    }

    public void TurnScreen(bool State)
    {
        LeftScreen.SetActive(State);
        RightScreen.SetActive(State);
    }

    public void TurnVR()
    {
        VREnvironments.transform.position = ObjectsPoint.transform.position;
        VREnvironments.transform.rotation = ObjectsPoint.transform.rotation;
        VREnvironments.SetActive(!VREnvironments.activeSelf);
        TrackingModule.LastContactedMenu = VREnvironments.gameObject.GetComponent<Canvas>();
    }
}
