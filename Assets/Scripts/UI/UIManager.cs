using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshairUI;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    [Header("Ammo UI")]
    [SerializeField] private Text ammoText;

    private CrosshairController crosshair;
    private HealthBarController health;
    private AmmoUIController ammo;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        crosshair = new CrosshairController(crosshairUI);
        health = new HealthBarController(healthBar);
        ammo = new AmmoUIController(ammoText);
    }



    public void SetCrosshairVisible(bool isVisivle) => crosshair.SetVisible(isVisivle);
    public void SetHealth(float normalizedValue) => health.SetHealth(normalizedValue);
    public void SetAmmoText(string text) => ammo.SetAmmo(text);



    // =============== UI SubModules ================

    private class CrosshairController
    {
        private GameObject crosshair;
        public CrosshairController(GameObject obj) => crosshair = obj;
        public void SetVisible(bool visible) => crosshair.SetActive(visible);
    }


    private class HealthBarController
    {
        private Slider bar;
        public HealthBarController(Slider slider) => bar = slider;
        public void SetHealth(float value) => bar.value = Mathf.Clamp01(value);
    }


    private class AmmoUIController
    {
        private Text ammo;
        public AmmoUIController(Text txt) => ammo = txt;
        public void SetAmmo(string value) => ammo.text = value;
    }
}
