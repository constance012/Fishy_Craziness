using UnityEngine;

public class Food : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] private Rigidbody2D rb2D;

	[Header("Food Settings"), Space]
	[SerializeField] private float maxFallSpeed;
	public float energyRecoverAmount;

	// Private fields.
	private float _fallSpeed;

	private void Start()
	{
		_fallSpeed = Random.Range(.5f, maxFallSpeed);
	}

	private void Update()
	{
		rb2D.velocity = _fallSpeed * Vector2.down;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("SeaFloor"))
		{
			Destroy(gameObject);
		}
	}
}