using UnityEngine;
using TMPro;

public class FishBase : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] protected Rigidbody2D rb2D;
	[SerializeField] protected Transform graphic;
	[SerializeField] private TextMeshProUGUI energyIndicator;

	[Header("Movement"), Space]
	[SerializeField] protected float maxSwimSpeed;
	[SerializeField] protected float maxTiltAngle;

	[Header("Energy"), Space]
	[SerializeField] protected float startEnergy = 10f;
	[SerializeField] protected float energyLossRate;

	// Properties.
	public float Energy { get; protected set; }

	// Protected fields.
	protected Vector2 _swimDirection;
	
	// Private fields.
	private float _fixedSwimSpeed;
	private bool _facingRight = true;

	protected virtual void Start()
	{
		Energy = startEnergy;

		if (energyIndicator != null)
		{
			energyIndicator.text = Energy.ToString("0");
			_swimDirection = new Vector2(-transform.position.x, 0f);
			_fixedSwimSpeed = Random.Range(2, maxSwimSpeed);
		}
	}

	protected virtual void Update()
	{
		Swim(_swimDirection, _fixedSwimSpeed);
		energyIndicator.text = Energy.ToString("0");

		if (Energy <= 0f)
		{
			Destroy(energyIndicator.gameObject);
			Destroy(gameObject);
		}
	}

	protected void Swim(Vector2 direction, float speed)
	{
		rb2D.velocity = direction.normalized * speed;
		CheckFlip();

		Energy = Mathf.Max(Energy - Time.deltaTime * energyLossRate, 0f);
	}

	protected void CheckFlip()
	{
		bool mustFlip = (_facingRight && rb2D.velocity.x < 0f) || (!_facingRight && rb2D.velocity.x >= 0f);

		if (mustFlip)
		{
			graphic.FlipByScale('x');
			_facingRight = !_facingRight;
		}
	}
}
