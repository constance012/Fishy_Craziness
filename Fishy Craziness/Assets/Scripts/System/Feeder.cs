using UnityEngine;

public class Feeder : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] private GameObject foodPrefab;

	[Header("Feeding Settings"), Space]
	[SerializeField] private float feedInterval;
	[SerializeField] private int foodPerFeed = 5;
	[SerializeField] private float maxSpawnHeight;

	// Private fields.
	private float _interval;

	private void Start()
	{
		_interval = feedInterval;
	}

	private void Update()
	{
		if (GameManager.Instance.IsGameOver)
			return;

		_interval -= Time.deltaTime;

		if (_interval <= 0f)
		{
			// Spawn the foods inside the view frustum of an orthographic camera.
			float vertialExtent = Camera.main.orthographicSize;
			float horizontalExtent = vertialExtent * Screen.width / Screen.height;

			for (int i = 0; i < foodPerFeed; i++)
			{
				float x = Random.Range(-horizontalExtent, horizontalExtent);
				float y = Random.Range(vertialExtent, maxSpawnHeight);

				Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
			}

			_interval = feedInterval;
		}
	}
}