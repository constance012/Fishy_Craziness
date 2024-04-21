using UnityEngine;

public sealed class BouncyFish : NormalFish
{
	protected override void Start()
	{
		animator.Play($"Bouncy Fish Variant {Random.Range(1, 5)}");

		_swimDirection = new Vector2(-transform.position.x, 0f);
		_fixedSwimSpeed = Random.Range(8f, maxSwimSpeed);
		_deadTimer = deadTimer;
	}

	private void Update()
	{
		Swim(_swimDirection, _fixedSwimSpeed);

		_deadTimer -= Time.deltaTime;

		if (_deadTimer <= 0f)
			Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
		{
			Vector3 reflect = Vector3.Reflect(_swimDirection, collision.contacts[0].normal).normalized;

			if (Vector3.Dot(_swimDirection.normalized, reflect) == -1f)
				_swimDirection = Quaternion.Euler(0f, 0f, 45 * Mathf.Sign(Random.value * 2f - 1f)) * reflect;
			else
				_swimDirection = reflect;
		}
		else if (collision.collider.CompareTag("Player"))
		{
			PlayerFish player = collision.collider.GetComponentInParent<PlayerFish>();
			player.UpdateEnergy((int)-player.Energy / 2);
			player.transform.position = Vector3.zero;

			Destroy(gameObject);
		}
	}
}