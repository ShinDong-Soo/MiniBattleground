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



    public void EquipWeapon(WeaponType weaponType)
    {
        CurrentWeaponType = weaponType;
        UIManager.Instance?.SetCrosshairVisible(weaponType == WeaponType.Gun);
    }
}
