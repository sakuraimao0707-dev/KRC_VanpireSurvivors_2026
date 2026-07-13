using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // “G‚ÌİŒv}
    public Transform player;        // Šî€‚É‚È‚éˆÊ’u
    public float interval = 2f;    // oŒ»ŠÔŠui•bj
    public float minInterval = 0.2f;
    public float radius = 12f;     // oŒ»‹——£
    float timer = 0f;
    int bonusHp = 0;
    float hpTimer = 0f;

    void Start()
    {
        //InvokeRepeating("Spawn", 1f, interval);
    }

    void Spawn()
    {
        Vector2 c = Random.insideUnitCircle.normalized * radius;
        Vector3 pos = player.position + new Vector3(c.x, 0f, c.y);
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemy.GetComponent<Enemy>().hp += bonusHp;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            Spawn();
            timer = 0f;
            interval = Mathf.Max(minInterval, interval - 0.05f);
        }

        hpTimer += Time.deltaTime;
        if (hpTimer >= 10f)
        {
            bonusHp++;
            hpTimer = 0f;
        }
    }
}