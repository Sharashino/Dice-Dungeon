using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
	[SerializeField] protected CanvasGroup menuCanvasGroup;
	public bool IsShown => menuCanvasGroup.IsShown();

	protected virtual void Awake()
	{
		if(menuCanvasGroup == null) menuCanvasGroup = GetComponent<CanvasGroup>();
	}

	public virtual void ShowHideMenu(bool state, GridBlock gridBlock)
	{
		if (state) menuCanvasGroup.Enable();
		else menuCanvasGroup.Disable();
	}
	
	public virtual void ShowHideMenu(bool state)
	{
		if (state) menuCanvasGroup.Enable();
		else menuCanvasGroup.Disable();
	}
}