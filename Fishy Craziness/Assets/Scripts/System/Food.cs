using UnityEngine;

public class Food : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] private Rigidbody2D rb2D;

	[Header("Food Settings"), Space]
	[SerializeField] private float fallSpeed;
	[SerializeField] private float energyRecoverAmount;

	private void Update()
	{
		rb2D.velocity = fallSpeed * Vector2.down;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			collider.GetComponentInParent<PlayerFish>().RecoverEnergy(energyRecoverAmount);
			Destroy(gameObject);
		}
		else if (collider.CompareTag("SeaFloor"))
		{
			Destroy(gameObject);
		}
	}
}