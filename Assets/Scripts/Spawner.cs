using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float spawnChance = 0.8f;
    
    private Vector2[] lanes = new Vector2[3] { new Vector2(-1.5f, 6f), new Vector2(0f, 6f), new Vector2(1.5f, 6f) };
    
    private float timer = 0f;
    
    private List<int> lanesUsed = new List<int>();
    
    void Start()
    {
        
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
        lanesUsed.Clear();
        for(int i = 0; i < lanes.Length; i++)
        {
            if(Random.value < spawnChance)
            {
                lanesUsed.Add(i);
            }
        }
        if (lanesUsed.Count == 0) {return;}
        if (lanesUsed.Count > 2)
        {
            lanesUsed.RemoveAt(Random.Range(0, lanesUsed.Count));
        }
        
        foreach (int lane in lanesUsed)
        {
            SpawnObstacle(lane);
        }
    }
    
    private void SpawnObstacle(int lane)
    {
        Obstacle obstacle = pool.GetObstacle("Block");
        obstacle.transform.position = lanes[lane];
        obstacle.gameObject.SetActive(true);
        obstacle.StartMoving(speed);
    }
}
