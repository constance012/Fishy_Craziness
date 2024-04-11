using UnityEngine;

public class EnergyIndicator : MonoBehaviour
{
    [Header("References"), Space]
    [SerializeField] private Transform worldPos;

    private static Canvas worldCanvas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ClearStatic()
    {
        worldCanvas = null;
    }

    private void Awake()
    {
        if (worldCanvas == null)
		{
			worldCanvas = GameObject.FindWithTag("WorldCanvas").GetComponent<Canvas>();
			worldCanvas.worldCamera = Camera.main;
		}
		
		transform.SetParent(worldCanvas.transform);
    }

    private void LateUpdate()
    {
        transform.position = worldPos.position;
    }
}
