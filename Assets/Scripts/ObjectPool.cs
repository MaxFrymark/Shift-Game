using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabs;
    
    private List<Obstacle> obstacles = new List<Obstacle>();

    public Obstacle GetObstacle(string tag)
    {
        Obstacle ob = null;
        foreach (var obstacle in obstacles)
        {
            if (obstacle.CompareTag(tag) && !obstacle.gameObject.activeSelf)
            {
                ob = obstacle;
                break;
            }
        }

        if (ob == null)
        {
            ob = SpawnNewObstacle(tag);
        }

        return ob;
    }
    
    public Obstacle SpawnNewObstacle(string tag)
    {
        Obstacle prefab = null;
        foreach (Obstacle obPrefab in obstaclePrefabs)
        {
            if (obPrefab.CompareTag(tag))
            {
                prefab = obPrefab;
                break;
            }
        }

        if (prefab == null)
        {
            Debug.LogError("Invalid Obstacle Tag: " + tag);
            return null;
        }

        Obstacle ob = Instantiate(prefab);
        ob.gameObject.SetActive(false);
        obstacles.Add(ob);
        return ob;
    }
}
