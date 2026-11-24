using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTag { Empty, Open, Block, Gem}

public class ItemHolder
{
    private List<ObstacleTag[]> items;
    private float blockChance;
    
    public ItemHolder(float blockChance)
    {
        this.blockChance = blockChance;
        Initialize();
    }

    public void SetBlockChance(float chance)
    {
        blockChance = chance;
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
        BuildRow(newRow);
    }

    private void BuildRow(ObstacleTag[] row)
    {
        //CheckIfPathBlocked(row);
        //CheckIfPathAccessible(row);
        SetOpenPath(row);
        AddBlocks(row);
        AddGem(row);
        items.Add(row);
    }

    private void SetOpenPath(ObstacleTag[] row)
    {
        if(items.Count == 0)
        {
            row[Random.Range(0, row.Length)] = ObstacleTag.Open;
            return;
        }
                
        List<int> openLanes = new List<int>();
        ObstacleTag[] previousRow = items[items.Count - 1];
        for(int i = 0; i < row.Length; i++)
        {
            if (previousRow[i] == ObstacleTag.Empty || previousRow[i] == ObstacleTag.Open || previousRow[i] == ObstacleTag.Gem)
            {
                openLanes.Add(i);
            }
        }
        int previousOpenLane = openLanes[Random.Range(0, openLanes.Count)];
        int shiftDir = Random.Range(-1, 1);
        int newOpenLane = Mathf.Clamp(previousOpenLane + shiftDir, 0, row.Length - 1);
        row[newOpenLane] = ObstacleTag.Open;
        
    }

    private void AddBlocks(ObstacleTag[] row)
    {
        for(int i = 0; i < row.Length; i++)
        {
            if(row[i] == ObstacleTag.Open) {continue;}
            if (Random.value < blockChance)
            {
                row[i] = ObstacleTag.Block;
            }
        }
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

    private void CheckIfPathAccessible(ObstacleTag[] row)
    {
        ObstacleTag[] previousRow = items[items.Count - 1];
        if(previousRow[1] == ObstacleTag.Empty || row[1] == ObstacleTag.Empty) {return;}
        if(previousRow[0] == row[0] && previousRow[2] == row[0]) {return;}

    }

    private void AddGem(ObstacleTag[] row)
    {
        List<int> openLanes = new List<int>();
        for(int i = 0; i < row.Length; i++)
        {
            if (row[i] == ObstacleTag.Empty || row[i] == ObstacleTag.Open)
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
