using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;   // GAME OVERの文字
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    int score = 0;
    float survivalTime = 0f;
    bool isGameOver = false;

    public TextMeshProUGUI levelText;   // ← LV表示
    int level = 1;                       // ← 今のレベル
    int nextLevelScore = 500;          // ← 次のレベルに必要なスコア

    public GameObject upgradePanel;   // ← 変数に追加：強化パネル
    public void AddScore(int point)
    {
        score += point;
        scoreText.text = "SCORE " + score;
        if (score >= nextLevelScore)   // ← 追加：関門を超えたか
        {
            LevelUp();                   // ← 追加
        }

    }

    public void GameOver()
    {
        gameOverText.SetActive(true);   // 文字を表示する
        Time.timeScale = 0f;          // 時間を止める
        isGameOver = true;
    }

    void Update()
    {
        survivalTime += Time.deltaTime;
        timeText.text = "TIME " + survivalTime.ToString("F1");
        if (isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;   // 時間を元に戻してから
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Time.timeScale != 0f)        // 0＝停止中は触らない
            Time.timeScale = Keyboard.current.leftShiftKey.isPressed ? 5f : 1f;

        if (isGameOver && Keyboard.current.tKey.wasPressedThisFrame)  // ← 追加
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
        }

    }

    void LevelUp()
    {
        level++;
        nextLevelScore = (int)(nextLevelScore * 1.5f);
        levelText.text = "LV " + level;
        Time.timeScale = 0f;             // ← 一時停止
        upgradePanel.SetActive(true);   // ← パネルを出す

        //level++;
        //nextLevelScore = (int)(nextLevelScore * 1.5f);  // 次の関門を1.5倍
        //levelText.text = "LV " + level;
        //Shooter shooter = FindAnyObjectByType<Shooter>();
        //shooter.interval *= 0.8f;            // 発射間隔を短く＝攻撃が速く
    }

    public void UpgradeAttackSpeed()   // 攻撃速度UP
    {
        FindAnyObjectByType<Shooter>().interval *= 0.8f;
        CloseUpgrade();
    }

    public void UpgradeMoveSpeed()     // 移動速度UP
    {
        FindAnyObjectByType<Player>().speed += 1f;
        CloseUpgrade();
    }

    void CloseUpgrade()            // 閉じて再開（共通）
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpgradeShotDirection()   // 攻撃方向ふやす
    {
        Shooter shooter = FindAnyObjectByType<Shooter>();
        shooter.shotCount = Mathf.Min(shooter.shotCount * 2, 32);
        CloseUpgrade();
    }
}