using UnityEngine;


public enum WeaponType
{
    None,
    Gun,
    Melee,
    Magic
}

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    private GameObject currentWeaponObject;
    private WeaponType currentWeaponType = WeaponType.None;

    public WeaponType CurrentWeaponType => currentWeaponType;
    private WeaponFire currentWeaponFire;



    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            currentWeaponFire?.Fire();
        }
    }



    public void EquipWeapon(WeaponData data)
    {
        if (currentWeaponObject != null)
            Destroy(currentWeaponObject);

        currentWeaponObject = Instantiate(data.weaponPrefab);
        currentWeaponObject.transform.SetParent(weaponHolder, false);
        currentWeaponObject.transform.localPosition = data.equipPositionOffset;
        currentWeaponObject.transform.localRotation = Quaternion.Euler(data.equipRotaionOffset);

        currentWeaponFire = currentWeaponObject.GetComponent<WeaponFire>();

        currentWeaponType = data.weaponType;
        UIManager.Instance?.SetCrosshairVisible(data.weaponType == WeaponType.Gun);
    }
}
