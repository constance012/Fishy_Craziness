using UnityEngine;
using TMPro;

public class NormalFish : FishBase
{
	[Header("References"), Space]
	[SerializeField] private TextMeshProUGUI energyIndicator;
	[SerializeField] protected Animator animator;

	[Header("Timer"), Space]
	[SerializeField, Min(0f)] protected float deadTimer;

	// Protected fields.
	protected float _fixedSwimSpeed;
	protected float _deadTimer;

	// Private fields.
	private static PlayerFish _player;

	private void Awake()
	{
		if (_player == null)
			_player = GameObject.FindWithTag("Player").GetComponentInParent<PlayerFish>();
	}

	protected override void Start()
	{
		Energy = Random.Range(_player.Energy - 5f, _player.Energy + 5f);
		Energy = Mathf.Clamp(Energy, 1f, 49f);

		energyIndicator.text = Energy.ToString("0");
		animator.Play($"Fish Variant {Random.Range(1, 12)}");

		_swimDirection = new Vector2(-transform.position.x, 0f);
		_fixedSwimSpeed = Random.Range(1f, maxSwimSpeed);
		_deadTimer = deadTimer;
	}

	private void Update()
	{
		Swim(_swimDirection, _fixedSwimSpeed);
		energyIndicator.text = _player.Energy >= this.Energy ? $"<color=\"green\">{this.Energy:0}" : $"<color=\"red\">{this.Energy:0}";

		if (Energy <= 0f)
		{
			_deadTimer -= Time.deltaTime;
			if (_deadTimer <= 0f)
				Kill();
		}
		else
			_deadTimer = deadTimer;
	}

	public void Kill()
	{
		Destroy(energyIndicator.gameObject);
		Destroy(gameObject);
	}
}