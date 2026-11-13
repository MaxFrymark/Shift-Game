using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float minSpeed = 8f;
    
    [SerializeField] private ObjectPool pool;
    
    [SerializeField] private float spawnInterval = 0.5f;
    
    [SerializeField] private float spawnChance = 0.8f;
    
    private Vector2[] lanes = new Vector2[3] { new Vector2(-1f, 5.5f), new Vector2(0f, 5.5f), new Vector2(1f, 5.5f) };
    
    private float timer = 0f;
    
    private ItemHolder itemHolder;
    
    void Start()
    {
        itemHolder = new ItemHolder(spawnChance);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            GenerateRow();
            timer = 0f;
        }
    }

    private void GenerateRow()
    {
        ObstacleTag[] row = itemHolder.GetNextRow();
        for(int i = 0; i < row.Length; i++)
        {
            if (row[i] != ObstacleTag.Empty)
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
}
