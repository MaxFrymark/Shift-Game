using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2[] lanes = new []{new Vector2(-1.5f, -4), new Vector2(0, -4), new Vector2(1.5f, -4)};
    private int currentLane = 1;
    
    public void ShiftLane(int shiftDirection)
    {
        
    }

    private void MoveToNewLane(int newLane)
    {
        currentLane = newLane;
        transform.position = lanes[newLane];
    }
}
