using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minSpeed = 8f;
    
    [SerializeField] private ObjectPool pool;
    
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnIntervalDecrease;
    [SerializeField] private float maxSpawnIntervalIncrease;

    private float currentSpawnInterval;
    
    [SerializeField] private float minSpawnChance;
    [SerializeField] private float maxSpawnChance;
    [SerializeField] private float maxSpawnChanceIncrease;
    [SerializeField] private float maxSpawnChanceDecrease;

    [SerializeField] private float baseSpeedChangeInterval;
    [SerializeField] private float speedChangeIntervalRange;
    private float currentSpeedChangeInterval;

    private float currentSpawnChance;
    
    private Vector2[] lanes = new Vector2[3] { new Vector2(-1f, 5.5f), new Vector2(0f, 5.5f), new Vector2(1f, 5.5f) };
    
    private float spawnTimer = 0f;
    private float speedChangeTimer = 0f;
    
    private ItemHolder _itemHolder;
    
    void Start()
    {
        currentSpawnInterval = maxSpawnInterval;
        currentSpawnChance = minSpawnChance;
        UpdateSpeedChangeInterval();
        _itemHolder = new ItemHolder(currentSpawnChance);
    }
    
    void Update()
    {
        spawnTimer += Time.deltaTime;
        speedChangeTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            GenerateRow();
            spawnTimer = 0f;
        }
        if (speedChangeTimer >= currentSpeedChangeInterval)
        {
            UpdateSpeed();
            UpdateSpeedChangeInterval();
            speedChangeTimer = 0f;
        }
    }

    private void GenerateRow()
    {
        ObstacleTag[] row = _itemHolder.GetNextRow();
        for(int i = 0; i < row.Length; i++)
        {
            if (row[i] != ObstacleTag.Empty && row[i] != ObstacleTag.Open)
            {
                SpawnObstacle(i, row[i]);
            }
        }
    }
    
    private void SpawnObstacle(int lane, ObstacleTag tag)
    {
        Obstacle obstacle = pool.GetObstacle(tag.ToString());
        obstacle.transform.position = lanes[lane];
        obstacle.gameObject.SetActive(true);
        obstacle.transform.SetParent(transform);
        obstacle.StartMoving(minSpeed);
    }

    private void UpdateSpeedChangeInterval()
    {
        currentSpeedChangeInterval = Random.Range(baseSpeedChangeInterval - speedChangeIntervalRange, 
            baseSpeedChangeInterval + speedChangeIntervalRange);
    }

    private void UpdateSpeed()
    {
        float intervalChange = Random.Range(-maxSpawnIntervalDecrease, maxSpawnIntervalIncrease);
        currentSpawnInterval = Mathf.Clamp(currentSpawnInterval + intervalChange, minSpawnInterval, maxSpawnInterval);
        float chanceChange = Random.Range(-maxSpawnChanceDecrease, maxSpawnChanceIncrease);
        currentSpawnChance = Mathf.Clamp(currentSpawnChance + chanceChange, minSpawnChance, maxSpawnChance);
        _itemHolder.SetBlockChance(currentSpawnChance);
    }
}
