using UnityEngine;

public class FirstTimeCheck : MonoBehaviour
{
    private static FirstTimeCheck instance;
    private static bool isFirstTime = true;

    public static bool IsFirstTime
    {
        get { return isFirstTime; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetFirstTime(bool value)
    {
        isFirstTime = value;
    }
}
