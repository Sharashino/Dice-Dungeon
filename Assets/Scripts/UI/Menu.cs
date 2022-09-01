using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
	[SerializeField] protected CanvasGroup menuCanvasGroup;
	public bool IsShown => menuCanvasGroup.alpha > 0;

	private void Awake()
	{
		menuCanvasGroup = GetComponent<CanvasGroup>();
	}

	public virtual void ShowHideMenu(bool state, object obj)
	{
		if (state) menuCanvasGroup.Enable();
		else menuCanvasGroup.Disable();
	}
}