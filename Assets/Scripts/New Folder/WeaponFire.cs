using UnityEngine;
using UnityEngine.Pool;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Bullet bulletPrefab;

    private IObjectPool<Bullet> bulletPool;


    private void Awake()
    {
        if (muzzlePoint != null)
        {
            muzzlePoint = transform.Find("MozzlePoint");
        }

        bulletPool = new ObjectPool<Bullet>(
            CreateBullet, OnGetBullet, OnRealseBullet, OnDestroyBullet,
            collectionCheck: false, defaultCapacity: 10, maxSize: 30);
    }


    public void Fire()
    {
        if (muzzlePoint == null || bulletPrefab == null) return;

        Bullet bullet = bulletPool.Get();
        bullet.transform.position = muzzlePoint.position;
        bullet.transform.rotation = muzzlePoint.rotation;
        bullet.gameObject.SetActive(true);
    }


    private Bullet CreateBullet()   
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }


    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);      
        bullet.Reset();
    }


    private void OnRealseBullet(Bullet bullet) => bullet.gameObject.SetActive(false);

    private void OnDestroyBullet(Bullet bullet) => Destroy(bullet.gameObject);
}
