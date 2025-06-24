using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private GameObject hitEffectPrefab;

    private float timer;
    private Rigidbody rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Fire(Vector3 direction)
    {
        gameObject.SetActive(true);
        rb.linearVelocity = direction * speed;
        timer = 0f;

        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            BulletPool.Instance.ReleaseBullet(this);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hitEffectPrefab != null)
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            GameObject effect = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(effect, 1.0f);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
        }

        BulletPool.Instance.ReleaseBullet(this);
    }
}
