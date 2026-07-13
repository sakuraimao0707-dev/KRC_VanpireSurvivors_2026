using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3f);  // 3ïbå„Ç…é©ï™Çè¡Ç∑
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);   // íeé©êgÇÕè¡Ç¶ÇÈ
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
