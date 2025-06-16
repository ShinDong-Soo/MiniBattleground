using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [System.Serializable]
    public class ZoomData
    {
        public CinemachineVirtualCamera VirtualCamera;
        [Range(0, 10f)] public float defaultDistance = 6f;
        [Range(0, 10f)] public float minDistance = 2f;
        [Range(0, 10f)] public float maxDistance = 6f;
        public float currentDistance;
        [HideInInspector] public CinemachineFramingTransposer transposer;
    }

    [SerializeField] private ZoomData[] zoomCameras;
    [SerializeField][Range(0, 10f)] private float smoothing = 4f;
    [SerializeField][Range(0, 10f)] private float zoomSensitivity = 1f;
    CinemachineInputProvider inputProvider;



    void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();

        foreach (var zoom in zoomCameras)
        {
            zoom.transposer = zoom.VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            zoom.currentDistance = zoom.defaultDistance;
            zoom.transposer.m_CameraDistance = zoom.defaultDistance;
        }
    }


    void Update()
    {
        float zoomInput = ReadZoomInput();
        UpdateActiveCameraZoom(zoomInput);
    }


    private float ReadZoomInput()
    {
        return inputProvider.GetAxisValue(2) * zoomSensitivity;
    }


    private void UpdateActiveCameraZoom(float zoomInput)
    {
        foreach (var zoom in zoomCameras)
        {
            if (!zoom.VirtualCamera.isActiveAndEnabled)
                continue;

            zoom.currentDistance = Mathf.Clamp(zoom.currentDistance + zoomInput, zoom.minDistance, zoom.maxDistance);
            float currentDistance = zoom.transposer.m_CameraDistance;

            if (Mathf.Approximately(currentDistance, zoom.currentDistance))
            {
                float lerpedDistance = Mathf.Lerp(currentDistance, zoom.currentDistance, smoothing * Time.deltaTime);
                zoom.transposer.m_CameraDistance = lerpedDistance;
            }
        }
    }
}
