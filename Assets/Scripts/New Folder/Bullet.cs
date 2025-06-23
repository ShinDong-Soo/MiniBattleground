using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private float speed = 30f;
    private float maxLifetime = 3f;
    private float currentLifetime;

    private IObjectPool<Bullet> pool;


    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool;
    }
}
