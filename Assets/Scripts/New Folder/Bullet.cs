using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float speed = 30f;
    private float lifetime = 3f;
    private float timer;

    private IObjectPool<Bullet> pool;
    private Rigidbody rb;


    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        timer = 0f;

        rb.linearVelocity = transform.position * speed;

    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            ReturnToPool();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
        }

        ReturnToPool();
    }



    private void ReturnToPool()
    {
        if (pool != null)
            pool.Release(this);
        else
            Destroy(gameObject);
    }



    public void Reset()
    {
        timer = 0f;
    }
}
