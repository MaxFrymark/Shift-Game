using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float spawnInterval = 3f;
    
    private Vector2[] lanes = new Vector2[3] { new Vector2(-1.5f, 6f), new Vector2(0f, 6f), new Vector2(1.5f, 6f) };
    
    private float timer = 0f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }
    
    private void SpawnObstacle()
    {
        Obstacle obstacle = pool.GetObstacle("Block");
        obstacle.transform.position = lanes[Random.Range(0, lanes.Length)];
        obstacle.gameObject.SetActive(true);
        obstacle.StartMoving(speed);
    }
}
