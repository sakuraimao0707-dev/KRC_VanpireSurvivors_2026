using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int hp = 2;
    Transform target;
    Rigidbody rb;
    GameManager gameManager;

    public GameObject explosionPrefab;   // ← 変数の並びに追加：爆発

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        //speed = Random.Range(2f, 8f);
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        rb.linearVelocity = dir.normalized * speed;
    }

    public void TakeDamage(int damage)
    {
        //hp -= damage;
        //if (hp <= 0)
        //{
        //    gameManager.AddScore(100);
        //    Destroy(gameObject);
        //}

        hp -= damage;
        if (hp <= 0)
        {
            gameManager.AddScore(100);
            Instantiate(explosionPrefab,             // ← この2行を追加
                        transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }



    //プレイヤーと接触したときにダメージを与える処理
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<Player>().TakeDamage(1);
    //        Destroy(gameObject);   // 弾自身は消える
    //    }
    //}
}
