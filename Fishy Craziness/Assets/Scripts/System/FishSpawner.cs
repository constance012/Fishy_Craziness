using UnityEngine;

public class FishSpawner : MonoBehaviour
{
	[Header("Fish Prefabs"), Space]
	[SerializeField] private GameObject normalFish;
	[SerializeField] private GameObject shark;

	[Header("Spawning Settings"), Space]
	[SerializeField] private float spawnInterval;
	[SerializeField] private float fishPerSpawn;
	[SerializeField] private Vector2 offScreenOffset;

	// Private fields.
	private float _interval;

	private void Start()
	{
		_interval = spawnInterval;
	}

	private void Update()
	{
		if (GameManager.Instance.IsGameOver)
			return;

		SpawnNormalFish();
	}

	private void SpawnNormalFish()
	{
		_interval -= Time.deltaTime;

		if (_interval <= 0f)
		{
			float vertialExtent = Camera.main.orthographicSize + offScreenOffset.y;
			float horizontalExtent = vertialExtent * Screen.width / Screen.height + offScreenOffset.x;

			for (int i = 0; i < fishPerSpawn; i++)
			{
				float x = horizontalExtent * Mathf.Sign(Random.value * 2f - 1f);
				float y = Random.Range(-vertialExtent, vertialExtent);

				Instantiate(normalFish, new Vector2(x, y), Quaternion.identity);
			}

			_interval = spawnInterval;
		}
	}
}