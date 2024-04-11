using UnityEngine;

public class PlayerFish : FishBase
{
	[Header("Damage Text"), Space]
	[SerializeField] private GameObject damageTextPrefab;
	protected override void Update()
	{
		Vector2 swimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb2D.position;
		float speed = Mathf.Lerp(1f, maxSwimSpeed, _energy / startEnergy);

		if (swimDirection.magnitude > .1f)
		{
			Swim(swimDirection, speed);
		}
		else
			rb2D.velocity = Vector2.zero;

		_energy = Mathf.Max(_energy - Time.deltaTime * energyLossRate, 0f);
		GameManager.Instance.UpdateEnergy(_energy);
	}

	public void RecoverEnergy(float amount)
	{
		_energy += amount;

		Vector2 damageTextPos = transform.position + Vector3.up;
		DamageText.Generate(damageTextPrefab, damageTextPos, DamageText.HealingColor, DamageTextStyle.Normal, $"+{amount}");

		GameManager.Instance.UpdateEnergy(_energy);
	}
}