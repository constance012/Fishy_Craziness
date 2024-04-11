using System.Timers;
using UnityEngine;

public class FishBase : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] protected Rigidbody2D rb2D;
	[SerializeField] protected Transform graphic;

	[Header("Movement"), Space]
	[SerializeField] protected float maxSwimSpeed;
	[SerializeField] protected float maxTiltAngle;

	[Header("Energy"), Space]
	[SerializeField] protected float startEnergy = 10f;
	[SerializeField] protected float energyLossRate;

	// Private fields.
	protected float _energy;
	private bool _facingRight = true;

	private void Start()
	{
		_energy = startEnergy;
	}

	protected virtual void Update()
	{
		// Autonomous Swimming...
	}

	protected void Swim(Vector2 direction, float speed)
	{
		rb2D.velocity = direction.normalized * speed;
		CheckFlip();
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
