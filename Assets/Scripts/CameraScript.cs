using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] GameObject choosenCamera;
    [SerializeField] private Vector3 cameraStartPosition;  
    
    [SerializeField] private float zoomStep = 0.1f;
    [SerializeField] private float maxZoomBond = 7f;
    [SerializeField] private float minZoomBond = 2f;

    private Camera cameraMain;

    private void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        choosenCamera.transform.position = cameraStartPosition + transform.position;
        ZoomCam();
    }
    
    private void ZoomCam()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cameraMain.orthographicSize < maxZoomBond)
        {
            cameraMain.orthographicSize += zoomStep;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cameraMain.orthographicSize > minZoomBond)
        {
            cameraMain.orthographicSize -= zoomStep;
        }
    }
        public void CamActivate()
    {
        if (isActive)
        { return; }
        foreach (CameraScript a in GameObject.FindObjectsOfType<CameraScript>())
        {
            a.isActive = false;
        }
        isActive = true;
    }
}
