using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    public Camera zoomcamera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the zoomcamera on this gameObject and the defaultZoom.
        zoomcamera = GetComponent<Camera>();
        if (zoomcamera)
        {
            defaultFOV = zoomcamera.fieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the zoomcamera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        zoomcamera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
