using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
	public override void UpdateBar(float value)
	{
		base.UpdateBar(value);
		SetHealthBar(maxValue);
	}
	
	public void SetHealthBar(float value)
	{
		barSlider.value = Mathf.Clamp(barSlider.value, value, maxValue);
	}
}
