using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    public float scrollSpeed;
    public int scrollMargin = 10;

    private int resolutionWidth;
    private int resolutionHeight;

    public int deZoomLimit = 10;
    public int zoomLimit = 2;
    public float zoomSpeed;

  

    private Camera myCamera;


    void Start()
    {
        myCamera = Camera.main;

        resolutionWidth = myCamera.pixelWidth;
        resolutionHeight = myCamera.pixelHeight;
    }

    
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        
        if (mousePos.y > resolutionHeight - scrollMargin && mousePos.y < resolutionHeight) myCamera.transform.position += scrollSpeed * Vector3.up * Time.deltaTime;
        if (mousePos.y < scrollMargin && mousePos.y > 0) myCamera.transform.position -= scrollSpeed * Vector3.up * Time.deltaTime;
        if (mousePos.x > resolutionWidth- scrollMargin && mousePos.x < resolutionWidth) myCamera.transform.position += scrollSpeed * Vector3.right * Time.deltaTime;
        if (mousePos.x < scrollMargin && mousePos.x > 0) myCamera.transform.position -= scrollSpeed * Vector3.right * Time.deltaTime;

        if (Input.mouseScrollDelta.y == 1) myCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
        if (Input.mouseScrollDelta.y == -1) myCamera.orthographicSize += zoomSpeed * Time.deltaTime;

        if (myCamera.orthographicSize > deZoomLimit) myCamera.orthographicSize = deZoomLimit;
        if (myCamera.orthographicSize < zoomLimit) myCamera.orthographicSize = zoomLimit;
        
    }
}
