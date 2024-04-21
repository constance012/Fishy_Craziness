using UnityEngine;

public abstract class FishBase : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] protected Rigidbody2D rb2D;

	[Header("Mobility"), Space]
	[SerializeField, Min(0f)] protected float maxSwimSpeed;

	[Header("Energy"), Space]
	[SerializeField, Min(1f)] protected float startEnergy = 10f;
	[SerializeField, Min(0f)] protected float energyLossRate;

	// Properties.
	public float Energy { get; protected set; }

	// Protected fields.
	protected Vector2 _swimDirection;
	
	// Private fields.
	private bool _facingRight = true;

	protected virtual void Start()
	{
		Energy = startEnergy;
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
			transform.Rotate(new Vector3(0f, 180f, 0f));
			_facingRight = !_facingRight;
		}
	}
}
