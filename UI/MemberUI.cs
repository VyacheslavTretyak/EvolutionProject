using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemberUI : AParamUI
{	
	public Text GeneNameText { get; set; }
	public AParam Param { get; private set; }
	public List<AParamUI> paramList;
	Color enabledColor;
	Color disabledColor;
	Button btn;
	void Awake()
	{
		paramList = new List<AParamUI>();
		size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
		selInfo = FindObjectOfType<SelectedInfo>();
		Text[] arr = GetComponentsInChildren<Text>();
		btn = GetComponentInChildren<Button>();
		GeneNameText = arr[0];
		enabledColor = btn.colors.normalColor;
		disabledColor = btn.colors.disabledColor;
	}
	private void OnGUI()
	{
		if (showLabel)
		{
			Vector3 v = Input.mousePosition;
			Rect r = new Rect(v.x - 20, Screen.height - v.y - 50, 150, 40);
			GUI.Box(r, Param.Name);
		}
	}
	public void OnClick()
	{		
		Param.IsActive = !Param.IsActive;
		ChangeColor();
		Own.Members.RedrawAll();
	}
	public void SetParam(AParam prm)
	{
		Param = prm;
		ChangeColor();
	}	
	private void ChangeColor()
	{
		ColorBlock cb = btn.colors;
		if (Param.IsActive)
		{
			cb.normalColor = enabledColor;
		}
		else
		{
			cb.normalColor = disabledColor;
		}
		btn.colors = cb;
		foreach (ParamUI item in paramList)
		{
			item.Button.interactable = Param.IsActive;
		}
	}
}
