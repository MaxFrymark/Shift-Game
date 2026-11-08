using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTag { Empty, Block, Gem}

public class ItemHolder
{
    private List<ObstacleTag[]> items;
    private float blockChance;
    
    public ItemHolder(float blockChance)
    {
        this.blockChance = blockChance;
        Initialize();
    }

    private void Initialize()
    {
        items = new List<ObstacleTag[]>();
        for (int i = 0; i < 10; i++)
        {
            AddRow();
        }
    }

    public ObstacleTag[] GetNextRow()
    {
        ObstacleTag[] rowToSend = items[0];
        items.RemoveAt(0);
        AddRow();
        return rowToSend;
    }

    private void AddRow()
    {
        ObstacleTag[] newRow = new ObstacleTag[3] {ObstacleTag.Empty, ObstacleTag.Empty, ObstacleTag.Empty};
        for(int i = 0; i < newRow.Length; i++)
        {
            if (Random.value < blockChance)
            {
                newRow[i] = ObstacleTag.Block;
            }
        }
        CheckIfRowValid(newRow);
    }

    private void CheckIfRowValid(ObstacleTag[] row)
    {
        CheckIfPathBlocked(row);
        AddGem(row);
        items.Add(row);
    }

    private void CheckIfPathBlocked(ObstacleTag[] row)
    {
        int blockCount = 0;
        for(int i = 0; i < row.Length; i++)
        {
            if (row[i] == ObstacleTag.Block)
            {
                blockCount++;
            }
        }

        if (blockCount >= 3)
        {
            int randomIndex = Random.Range(0, row.Length);
            row[randomIndex] = ObstacleTag.Empty;
        }
    }

    private void AddGem(ObstacleTag[] row)
    {
        List<int> openLanes = new List<int>();
        for(int i = 0; i < row.Length; i++)
        {
            if (row[i] == ObstacleTag.Empty)
            {
                openLanes.Add(i);
            }
        }
        
        if(openLanes.Count > 0)
        {
            int randomIndex = Random.Range(0, openLanes.Count);
            row[openLanes[randomIndex]] = ObstacleTag.Gem;
        }
    }
}
