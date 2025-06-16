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

    [Header("Zoom Settings")]
    [SerializeField] private ZoomData[] zoomCameras;
    [SerializeField][Range(0, 10f)] private float smoothing = 4f;
    [SerializeField][Range(0, 10f)] private float zoomSensitivity = 1f;

    [Header("Input")]
    [SerializeField] private CinemachineInputProvider inputProvider;



    void Awake()
    {
        if (inputProvider == null)
        {
            Debug.LogError("CinemachineInputProvider가 할당되지 않았습니다.");
        }

        foreach (var zoom in zoomCameras)
        {
            if (zoom.VirtualCamera == null)
            {
                Debug.LogError("VirtualCamera가 할당되지 않았습니다.");
                continue;
            }

            zoom.transposer = zoom.VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            if (zoom.transposer == null)
            {
                Debug.LogError("FramingTransposer가 없습니다.");
                continue;
            }

            zoom.currentDistance = zoom.defaultDistance;
            zoom.transposer.m_CameraDistance = zoom.defaultDistance;
        }
    }


    void Update()
    {
        if (inputProvider == null) return;

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

            if (!Mathf.Approximately(currentDistance, zoom.currentDistance))
            {
                float lerpedDistance = Mathf.Lerp(currentDistance, zoom.currentDistance, smoothing * Time.deltaTime);
                zoom.transposer.m_CameraDistance = lerpedDistance;
            }
        }
    }
}
