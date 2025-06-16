using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController Instance { get; private set; }

    [SerializeField] private GameObject crosshairUI;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    public void SetCrosshairVisible(bool isVisible)
    {
        if (crosshairUI != null)
            crosshairUI.SetActive(isVisible);
    }
}
