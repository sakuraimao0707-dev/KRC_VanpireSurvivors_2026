using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;   // 弾の設計図
    public float interval = 0.5f;     // 発射間隔（秒）
    public float bulletSpeed = 12f;   // 弾の速さ

    float timer = 0f;        // ← 追加：自前タイマー
    // Start内のInvokeRepeating行を削除（Startは空のまま残す）

    public int shotCount = 1;   // ← 追加：弾の向きの数

    public AudioClip shootClip;   // ← 追加：発射音
    AudioSource audioSource;      // ← 追加：スピーカー

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //InvokeRepeating("Shoot", 1f, interval);
    }

    void Update()          // ← 追加
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
         GameObject target = FindNearestEnemy();
        if (target == null) { return; }
        Vector3 baseDir = target.transform.position - transform.position;
        baseDir.y = 0f;
        baseDir = baseDir.normalized;
        for (int i = 0; i < shotCount; i++)
        {
            float ang = i * 360f / shotCount;
            Vector3 dir = Quaternion.AngleAxis(ang, Vector3.up) * baseDir;
            GameObject b = Instantiate(bulletPrefab,
                           transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().linearVelocity = dir * bulletSpeed;
        }
        audioSource.PlayOneShot(shootClip);


        //GameObject target = FindNearestEnemy();
        //if (target == null) { return; }  // 敵がいなければ撃たない
        //
        //Vector3 dir = target.transform.position - transform.position;
        //dir.y = 0f;
        //
        //GameObject bullet = Instantiate(bulletPrefab,
        //                    transform.position, Quaternion.identity);
        //
        //Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //rb.linearVelocity = dir.normalized * bulletSpeed;
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;  // 最初は「無限に遠い」

        foreach (GameObject e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);
            if (d < minDist)
            {
                minDist = d;     // いちばん近い記録を更新
                nearest = e;
            }
        }
        return nearest;
    }


}
