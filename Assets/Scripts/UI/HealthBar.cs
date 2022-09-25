using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
	public static Action<float> OnHealthChanged;

	private void Awake()
	{
		OnHealthChanged += UpdateHealth;
	}

	private void UpdateHealth(float value)
	{
		// TODO Add health bar smoothness
		barSlider.value = Mathf.Clamp(value, 0, maxValue);
	}
}
