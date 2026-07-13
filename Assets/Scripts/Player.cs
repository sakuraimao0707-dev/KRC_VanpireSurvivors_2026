using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 6f;

    //public int hp = 3;   // 体力

    public GameManager gameManager;   // 管理係

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = Keyboard.current;

        float x = 0f;
        float z = 0f;
        if (kb.aKey.isPressed) { x = -1f; }
        if (kb.dKey.isPressed) { x = 1f; }
        if (kb.sKey.isPressed) { z = -1f; }
        if (kb.wKey.isPressed) { z = 1f; }

        Vector3 dir = new Vector3(x, 0f, z);
        rb.linearVelocity = dir.normalized * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();   // 管理係に知らせる
        }
    }

    //敵と当たった時にダメージを受ける処理（Enemey スクリプトのコメントアウトしているところと一緒に使う）
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        TakeDamage(1);
    //        // gameManager.GameOver();   // 管理係に知らせる
    //    }
    //}
    //
    //public void TakeDamage(int damage)
    //{
    //    hp -= damage;
    //    if (hp <= 0)
    //    {
    //        gameManager.GameOver();
    //    }
    //}
}

