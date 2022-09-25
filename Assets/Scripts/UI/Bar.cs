using System;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
	[SerializeField] protected Slider barSlider;
	[SerializeField] protected float maxValue;

	protected virtual void UpdateBar(float val)
	{
		maxValue = val;
		barSlider.maxValue = maxValue;
	}
}