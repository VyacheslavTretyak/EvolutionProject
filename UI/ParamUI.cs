using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParamUI : AParamUI
{
	public AParamValue Param { get; set; }
	public Text ParamNameText { get; set; }
	public Text ParamValText { get; set; }

	public Button Button { get; set; }
	Animator plusAnim;
	Animator minusAnim;
	
	void Awake () {
		size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
		selInfo = FindObjectOfType<SelectedInfo>();
		Text[] arr = GetComponentsInChildren<Text>();
		Button = GetComponentInChildren<Button>();
		ParamNameText = arr[0];
		ParamValText = arr[1];		
		plusAnim = GetComponentInChildren<PlusBtn>().GetComponent<Animator>();
		minusAnim = GetComponentInChildren<MinusBtn>().GetComponent<Animator>();
		plusAnim.enabled = false;
		minusAnim.enabled = false;
	}
	public void AppearBtn()
	{
		plusAnim.enabled = true;
		minusAnim.enabled = true;
		plusAnim.Play("AppearBtn");
		minusAnim.Play("AppearBtn");
	}
	public void DisappearBtn()
	{		
		plusAnim.Play("DisappearBtn");
		minusAnim.Play("DisappearBtn");
	}
	public void Plus()
	{
		Param.Increase();
		ParamValText.text = Param.ToString();
		Own.Members.RedrawAll();		
	}
	public void Minus()
	{
		Param.Decrease();
		ParamValText.text = Param.ToString();
		Own.Members.RedrawAll();
	}
	private void OnGUI()
	{
		if (showLabel)
		{
			Vector3 v = Input.mousePosition;			
			Rect r = new Rect(v.x - 20, Screen.height - v.y - 50, 150, 40);
			GUI.Box(r, Param.FullName);
		}
	}
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (Button.interactable)
		{
			showLabel = true;
			AppearBtn();
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		showLabel = false;
		DisappearBtn();
	}


}

