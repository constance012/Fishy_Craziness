using UnityEngine;

public sealed class PlayerFish : FishBase
{
	[Header("Damage Text"), Space]
	[SerializeField] private GameObject damageTextPrefab;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
	{
		_swimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb2D.position;
		float speed = Mathf.Lerp(2f, maxSwimSpeed, Energy / startEnergy);

		if (_swimDirection.magnitude > .1f)
		{
			Swim(_swimDirection, speed);
		}
		else
			rb2D.velocity = Vector2.zero;

		GameManager.Instance.UpdatePlayerEnergy(Energy);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Food"))
		{
			UpdateEnergy(collider.GetComponent<Food>().energyRecoverAmount);
			Destroy(collider.gameObject);
		}
		else if (collider.CompareTag("OtherFish"))
		{
			NormalFish other = collider.GetComponentInParent<NormalFish>();
			
			if (other.Energy <= this.Energy)
			{
				float amount = (int)other.Energy % 2 == 0 ? 2f : 1f;
				UpdateEnergy(amount);
			}
			else
			{
				UpdateEnergy(-2f);
			}

			other.Kill();
		}
	}

	public void UpdateEnergy(float delta)
	{
		Energy = Mathf.Clamp(Energy + delta, 1f, 49f);

		string content = delta < 0 ? $"{delta}" : $"+{delta}";
		Vector2 damageTextPos = transform.position + Vector3.up;
		Color color = delta < 0 ? DamageText.DamageColor : DamageText.HealingColor;

		DamageText.Generate(damageTextPrefab, damageTextPos, color, DamageTextStyle.Normal, content);

		GameManager.Instance.UpdatePlayerEnergy(Energy);
	}
}