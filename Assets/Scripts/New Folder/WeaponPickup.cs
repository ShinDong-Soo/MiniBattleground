using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var weaponController = other.GetComponent<WeaponController>();
            if (weaponController != null)
            {
                weaponController.EquipWeapon(weaponData);
                Destroy(gameObject);
            }
        }
    }
}
