using UnityEngine;
using UnityEngine.Pool;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float fireRate = 0.2f;

    private float lastFireTime;



    public void Fire()
    {
        if (Time.time < lastFireTime + fireRate) return;

        if (muzzlePoint == null)
        {
            Debug.LogWarning("Muzzle point not assigned");
            return;
        }

        Bullet bullet = BulletPool.Instance.GetBullet();
        bullet.transform.position = muzzlePoint.position;
        bullet.transform.rotation = muzzlePoint.rotation;
        bullet.Fire(muzzlePoint.forward);

        lastFireTime = Time.time;
    }
}
