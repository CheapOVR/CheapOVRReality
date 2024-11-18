using Mediapipe.Unity;
using Mediapipe.Unity.Sample.HandTracking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public MainTrackingModule _MainTrackingModule;
    PointAnnotation ColliderPointAnnotation;

    private bool Cooldown;
    void Start()
    {
        SphereCollider ColliderSphereCollider = this.gameObject.GetComponent<SphereCollider>();
        Rigidbody ColliderRigidBody = this.gameObject.AddComponent<Rigidbody>();
        ColliderPointAnnotation = this.gameObject.AddComponent<PointAnnotation>();
        ColliderRigidBody.isKinematic = true;
        ColliderRigidBody.useGravity = false;
        ColliderSphereCollider.isTrigger = true;
        InvokeRepeating("Update_Every_1000ms", 2, 1f);
    }
    void Update_Every_1000ms ()
    {
        if (Cooldown) Cooldown = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.CompareTag("Interface") && _MainTrackingModule.IsListActive)
        {
            Debug.Log($"Trigger with! {other.name}.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_MainTrackingModule.IsListActive && !Cooldown)
        {
            if (other.CompareTag("Interface")) // If there interface on a way
            {
                if (!other.gameObject.GetComponentInParent<Canvas>().CompareTag("SafePane")) _MainTrackingModule.LastContactedMenu = other.gameObject.GetComponentInParent<Canvas>();
                ExecuteEvents.Execute(other.gameObject.GetComponent<Button>().gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler); // Do clicking on a button (If its a button) by emulating mouse event
                Cooldown = true;
            }
            else if (other.CompareTag("ScreenCast")) // If there is a raycast...
            {
                //ClientScript.Finger = ScreenCaster.GetComponent<RectTransform>().rect.GetPoint(_currentHandLandmarkLists[0].Landmark[8]);
            }
            if (other.CompareTag("ThumbFinger") && _MainTrackingModule.IsListActive)
            {
                Debug.Log($"Trigger with Thumb Finger.");
                _MainTrackingModule.ChangePositionOfWindow();
                Cooldown = true;
            }
        }
    }
}
