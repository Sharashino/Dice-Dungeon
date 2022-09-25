using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : Bar
{
	public static Action<float> OnManaChanged;

	private void Awake()
	{
		OnManaChanged += UpdateMana;
	}

	private void UpdateMana(float value)
	{
		// TODO Add mana bar smoothness
		barSlider.value = Mathf.Clamp(value, 0, maxValue);
	}
}
