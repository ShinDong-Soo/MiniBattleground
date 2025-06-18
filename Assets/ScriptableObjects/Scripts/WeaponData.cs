using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon System/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    public int damage;
    public float range;
    public GameObject weaponPrefab;

    public Vector3 equipPositionOffset;
    public Vector3 equipRotaionOffset;
}
