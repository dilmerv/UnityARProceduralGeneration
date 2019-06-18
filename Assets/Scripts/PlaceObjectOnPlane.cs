using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{   
    [SerializeField]
    private ParamsBinding paramsBinding;

    [SerializeField, Tooltip("Object to move around the plane")]
    private GameObject placedObject;

    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        placedObject.SetActive(false);
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void OnEnable() 
    {
        if(placedObject == null)
        {
            Debug.LogError("You must set placedObject in order to use this class");
            enabled = false;
        }    
    }

    void Update()
    {
        if (Input.touchCount > 0 && !paramsBinding.IsParamsAreaVisible)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    placedObject.transform.position = hitPose.position;
                    placedObject.transform.rotation = hitPose.rotation;
                    placedObject.SetActive(true);

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
    }
}
