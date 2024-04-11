using System.Runtime;
using UnityEngine;

public class PlayerFish : FishBase
{
	[Header("Damage Text"), Space]
	[SerializeField] private GameObject damageTextPrefab;

	// Private fields.
	private Vector2 _damageTextPos;

    protected override void Start()
    {
        base.Start();
		_damageTextPos = transform.position + Vector3.up;
    }

    protected override void Update()
	{
		_swimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb2D.position;
		float speed = Mathf.Lerp(1f, maxSwimSpeed, Energy / startEnergy);

		if (_swimDirection.magnitude > .1f)
		{
			Swim(_swimDirection, speed);
		}
		else
			rb2D.velocity = Vector2.zero;

		GameManager.Instance.UpdateEnergy(Energy);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Food"))
		{
			RecoverEnergy(collider.GetComponent<Food>().EnergyRecoverAmount);
			Destroy(collider.gameObject);
		}
		else if (collider.CompareTag("OtherFish"))
		{
			FishBase other = collider.GetComponentInParent<FishBase>();
			Debug.Log(other == null);
			
			if (other.Energy <= this.Energy)
			{
				float amount = other.Energy % 2f == 0 ? 2f : 1f;
				RecoverEnergy(amount);
			}
			else
			{
				this.Energy = Mathf.Max(this.Energy - 2f, 0f);
				DamageText.Generate(damageTextPrefab, _damageTextPos, DamageText.DamageColor, DamageTextStyle.Normal, -2f);
			}

			Destroy(collider.gameObject);
		}
	}

	public void RecoverEnergy(float amount)
	{
		Energy += amount;

		DamageText.Generate(damageTextPrefab, _damageTextPos, DamageText.HealingColor, DamageTextStyle.Normal, $"+{amount}");

		GameManager.Instance.UpdateEnergy(Energy);
	}
}