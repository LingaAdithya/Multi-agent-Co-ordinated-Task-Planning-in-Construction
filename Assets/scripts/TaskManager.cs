using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [Header("Task State")]
    public bool materialDelivered = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void MarkMaterialDelivered()
    {
        materialDelivered = true;
        Debug.Log("✅ Material Delivered. Assembly can start.");
    }
}
