// using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject obstacleBar;
    
    // Inspectorで3種類の壁プレハブを設定する
    [SerializeField] private GameObject[] wallObstacles; 

    [SerializeField] private float spawnRate = 3f;
    private float timer = 0;
    private float sumTimer = 0;
    private const float spawnDistance = 15f; // 生成位置のZ座標


    void Start()
    {
        if (obstacleBar == null)
        {
            Debug.LogError("障害物バーのプレハブがInspectorで設定されていません。");
        }

        if (wallObstacles == null || wallObstacles.Length == 0)
        {
            Debug.LogError("壁のプレハブがInspectorで設定されていません。");
        }
    }


    void Update()
    {
        if (Time.timeScale == 0) return; // ゲームが停止していたら何もしない

        timer += Time.deltaTime;
        sumTimer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        if (sumTimer < 10f)
        {
            SpawnBar();
        }
        else if (sumTimer < 20f)
        {
            SpawnWall();
        }
        else
        {
            SpawnWall();
            SpawnBar();
        }
    }

    void SpawnBar()
    {
        // 障害物バーのプレハブが設定されているか確認
        if (obstacleBar == null)
        {
            Debug.LogError("障害物バーのプレハブがInspectorで設定されていません。");
            return;
        }
        // 上段と下段の高さをランダムに選ぶ
        float[] spawnHeights = { 1.6f, 0.6f }; 

        // ランダムに高さを選ぶ
        float randomHeight = spawnHeights[Random.Range(0, spawnHeights.Length)];

        // 障害物バーの生成位置（Y座標はランダムに選んだ高さ、Z座標は一定）
        Vector3 spawnPos = new Vector3(0, randomHeight, spawnDistance);

        Quaternion spawnRotation = Quaternion.Euler(0, 0, 90);
        Instantiate(obstacleBar, spawnPos, spawnRotation);
    }

    void SpawnWall()
    {
        // 配列にプレハブが設定されているか確認
        if (wallObstacles == null || wallObstacles.Length == 0)
        {
            Debug.LogError("壁のプレハブがInspectorで設定されていません。");
            return;
        }

        // 3種類の壁プレハブからランダムに1つ選ぶ
        GameObject wallToSpawn = wallObstacles[Random.Range(0, wallObstacles.Length)];
        
        // 壁の生成位置（Y座標はプレハブの高さに合わせて調整してください）
        Vector3 spawnPos = new Vector3(0, 1.42f, spawnDistance);
        
        // 壁は回転させない
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);
        
        Instantiate(wallToSpawn, spawnPos, spawnRotation);
    }
}