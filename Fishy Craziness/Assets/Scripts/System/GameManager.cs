using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	[Header("UI References"), Space]
	[SerializeField] private TextMeshProUGUI energyText;
	[SerializeField] private TextMeshProUGUI timeText;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private TextMeshProUGUI totalTimeText;

	[Header("Game Object References"), Space]
	[SerializeField] private PlayerFish player;

	[Header("Other Settings"), Space]
	[SerializeField] private float gameOverDelay = 3f;
	
	// Properties
	public bool IsGameOver { get; private set; }

	// Private fields.
	private float _gameOverDelay;

	private void Start()
	{
		_gameOverDelay = gameOverDelay;
	}

	private void Update()
	{
		if (IsGameOver)
			return;

		timeText.text = $"Time: {Time.timeSinceLevelLoad:0}";
	}

	public void UpdatePlayerEnergy(float energy)
	{
		energyText.text = $"Energy: {energy:0}";

		if (energy <= 0f)
		{
			_gameOverDelay -= Time.deltaTime;
			if (_gameOverDelay <= 0f)
				GameOver();
		}
		else
			_gameOverDelay = gameOverDelay;
	}

	public void GameOver()
	{
		Debug.LogWarning("Game Over!!!");
		
		gameOverPanel.SetActive(true);
		totalTimeText.text = $"Total Time: <color=\"red\">{Time.timeSinceLevelLoad:0} secs";
		player.enabled = false;

		IsGameOver = true;
	}

	// Callback method for the Retry Button.
	public void Restart()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
