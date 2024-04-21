using UnityEngine;

public class EnergyIndicator : MonoBehaviour
{
    [Header("References"), Space]
    [SerializeField] private Transform worldPos;

    private static Canvas _worldCanvas;

    private void Awake()
    {
        if (_worldCanvas == null)
		{
			_worldCanvas = GameObject.FindWithTag("WorldCanvas").GetComponent<Canvas>();
			_worldCanvas.worldCamera = Camera.main;
		}
		
		transform.SetParent(_worldCanvas.transform);
    }

    private void LateUpdate()
    {
        transform.position = worldPos.position;
    }
}
