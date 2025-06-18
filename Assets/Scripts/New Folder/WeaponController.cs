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
    public WeaponType CurrentWeaponType { get; private set; } = WeaponType.None;
    [SerializeField] private Transform weaponHolder;
    private GameObject currentWeaponObject;



    public void EquipWeapon(WeaponData data)
    {
        if (currentWeaponObject != null)
            Destroy(currentWeaponObject);

        currentWeaponObject = Instantiate(data.weaponPrefab, weaponHolder);

        currentWeaponObject.transform.localPosition = data.equipPositionOffset;
        currentWeaponObject.transform.localRotation = Quaternion.Euler(data.equipRotaionOffset);
        
        CurrentWeaponType = data.weaponType;
        UIManager.Instance?.SetCrosshairVisible(data.weaponType == WeaponType.Gun);
    }
}
