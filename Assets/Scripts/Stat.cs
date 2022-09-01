using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	[SerializeField] private string statName = default; // Stat name 
	[SerializeField] private int baseValue = default; // Stat base value 

	public List<int> modifiers = new List<int>(); // List of modifiers to this stat

	public string StatName => statName;

	// Base value = stat modifiers + base value
	public int BaseValue {
		get
		{
			var finalValue = baseValue;
			modifiers.ForEach(x => finalValue += x);

			return finalValue;
		}
		set => baseValue = value;
	}

	// Add modifier to stat
	public void AddModifier(int modifier)
	{
		if (modifier != 0) modifiers.Add(modifier);
	}

	// Remove modifier from stat
	public void RemoveModifier(int modifier)
	{
		if (modifier != 0) modifiers.Remove(modifier);
	}
}