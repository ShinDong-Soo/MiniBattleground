using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private Bullet bulletPrefab;

    private ObjectPool<Bullet> bulletPool;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        bulletPool = new ObjectPool<Bullet>(
            () => Instantiate(bulletPrefab),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => { },
            collectionCheck: true, 
            defaultCapacity :50, 
            maxSize: 200
        );
    }



    private void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            Bullet bullet = bulletPool.Get();
            bulletPool.Release(bullet);
        }
    }


    public Bullet GetBullet()
    {
        return bulletPool.Get();
    }

    public void ReleaseBullet(Bullet bullet) => bulletPool.Release(bullet);
}
