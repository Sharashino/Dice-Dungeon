using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
	[SerializeField] private string statName; // Stat name 
	[SerializeField] private float baseValue; // Stat base value 

	public List<float> modifiers = new(); // List of modifiers to this stat

	public string StatName => statName;

	// Base value = stat modifiers + base value
	public float BaseValue {
		get
		{
			var finalValue = baseValue;
			modifiers.ForEach(x => finalValue += x);

			return finalValue;
		}
		set => baseValue = value;
	}

	// Add modifier to stat
	public void AddModifier(float modifier)
	{
		if (modifier != 0) modifiers.Add(modifier);
	}

	// Remove modifier from stat
	public void RemoveModifier(float modifier)
	{
		if (modifier != 0) modifiers.Remove(modifier);
	}
}