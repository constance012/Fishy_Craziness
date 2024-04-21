using UnityEngine;

public class FishSpawner : MonoBehaviour
{
	[Header("Fish Prefabs"), Space]
	[SerializeField] private GameObject normalFish;
	[SerializeField] private GameObject bouncyFish;

	[Header("Normal Fish Spawning Settings"), Space]
	[SerializeField] private float spawnInterval;
	[SerializeField] private float fishPerSpawn;

	[Header("Bouncy Fish Spawning Settings"), Space]
	[SerializeField] private Vector2 bouncyFishSpawnInterval;
	[SerializeField, Tooltip("The offscreen offset to spawn fish, x is for horizontal, y and z is for vertical")]
	private Vector3 offScreenOffset;

	// Private fields.
	private float _interval;
	private float _nextBouncyFishSpawnTime;

	private void Start()
	{
		_interval = spawnInterval;
		_nextBouncyFishSpawnTime = Time.timeSinceLevelLoad + Random.Range(bouncyFishSpawnInterval.x, bouncyFishSpawnInterval.y);
	}

	private void Update()
	{
		if (GameManager.Instance.IsGameOver)
			return;

		SpawnNormalFish();
		SpawnBouncyFish();
	}

	private void SpawnNormalFish()
	{
		_interval -= Time.deltaTime;

		if (_interval <= 0f)
		{
			float cameraYExtent = Camera.main.orthographicSize;
			Vector2 verticalExtent = new Vector2(-cameraYExtent + offScreenOffset.y, cameraYExtent + offScreenOffset.z);
			float horizontalExtent = cameraYExtent * Screen.width / Screen.height + offScreenOffset.x;

			for (int i = 0; i < fishPerSpawn; i++)
			{
				float x = horizontalExtent * Mathf.Sign(Random.value * 2f - 1f);
				float y = Random.Range(verticalExtent.x, verticalExtent.y);

				Instantiate(normalFish, new Vector2(x, y), Quaternion.identity);
			}

			_interval = spawnInterval;
		}
	}

	private void SpawnBouncyFish()
	{
		if (Time.timeSinceLevelLoad >= _nextBouncyFishSpawnTime)
		{
			float cameraYExtent = Camera.main.orthographicSize;
			Vector2 verticalExtent = new Vector2(-cameraYExtent + offScreenOffset.y, cameraYExtent + offScreenOffset.z);
			float horizontalExtent = cameraYExtent * Screen.width / Screen.height + offScreenOffset.x;

			float x = horizontalExtent * Mathf.Sign(Random.value * 2f - 1f);
			float y = Random.Range(verticalExtent.x, verticalExtent.y);

			Instantiate(bouncyFish, new Vector2(x, y), Quaternion.identity);

			_nextBouncyFishSpawnTime += Random.Range(bouncyFishSpawnInterval.x, bouncyFishSpawnInterval.y);
		}
	}
}