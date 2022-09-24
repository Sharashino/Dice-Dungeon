using System;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
	public static Action<float> OnBarChanged;

	[SerializeField] protected Slider barSlider;
	[SerializeField] protected float maxValue;

	private void Awake()
	{
		barSlider = GetComponent<Slider>();
	}

	public virtual void UpdateBar(float val)
	{
		maxValue = val;
		barSlider.maxValue = maxValue;
	}
}