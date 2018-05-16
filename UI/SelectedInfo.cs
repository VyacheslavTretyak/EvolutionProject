using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfo : MonoBehaviour
{
	public ParamUI prefParamUI;
	public MemberUI prefMemberUI;
	public Creature Selected { get; set; }

	List<MemberUI> paramElList;
	
	Camera camGUI;	
	Animator slideAnimator;
	ParamPanel paramPanel;
	bool uplockParam = false;
	private void Start()
	{
		slideAnimator = GetComponent<SelectedInfo>().GetComponent<Animator>();
		slideAnimator.enabled = false;

		paramElList = new List<MemberUI>();
		paramPanel = GetComponentInChildren<ParamPanel>();
		camGUI = FindObjectOfType<CreatureCamera>().GetComponent<Camera>();
	}
	void ClearParamList()
	{
		if (paramElList.Count == 0)
		{
			return;
		}
		foreach (MemberUI fild in paramElList)
		{
			foreach (ParamUI item in fild.paramList)
			{
				Destroy(item.gameObject);
			}
			Destroy(fild.gameObject);
		}
		paramElList.Clear();
	}
	public void SetSelected(Creature cr)
	{
		if (cr == null && Selected != null)
		{
			Selected.Members.SetLayer(0);
			Selected = null;
			ClearParamList();
			camGUI.enabled = false;			
			slideAnimator.Play("SlidePanelOut");			
		}
		else if (Selected != cr)
		{
			Selected?.Members.SetLayer(0);
			Selected = cr;
			uplockParam = true;//TODO ON/OFF
			Reproduction repr = Selected.Behaviors.GetBehavior<Reproduction>();
			if (repr != null)
			{
				if (repr.IsPregnant)
				{					
					ShowCreatureOnPanel(repr.Baby);
					uplockParam = true;
				}
				else
				{
					ShowCreatureOnPanel(Selected);
				}
			}			
		}
	}
	private void ShowCreatureOnPanel(Creature creature)
	{		
		creature.Members.SetLayer(8);
		camGUI.enabled = true;
		camGUI.transform.SetParent(creature.transform, false);
		GetComponentInChildren<CreatureName>().GetComponent<Text>().text = creature.ToString();
		GetComponentInChildren<GenerationText>().GetComponent<Text>().text = creature.Generation.ToString();
		GetComponentInChildren<KindText>().GetComponent<Text>().text = creature.Kind.ToString();
		GetComponentInChildren<GenderPic>().SetPicture(creature);
		slideAnimator.enabled = true;
		slideAnimator.Play("SlidePanelIn");
		DrawMembersUI();
	}
	float DrawParamsUI(MemberUI memUI)
	{
		AParam mem = memUI.Param;
		Vector2 pos = memUI.Position;
		float sy = 0;
		for (int i = 0; i < mem.paramsList.Count; i++)
		{			
			ParamUI pu = Instantiate(prefParamUI);
			pu.Own = Selected;
			pu.Button.interactable = memUI.Param.IsActive;
			pu.transform.SetParent(paramPanel.transform);
			pos.y += pu.Size.y + 5;
			sy = pu.Size.y;
			pu.GetComponent<RectTransform>().anchoredPosition = pos;
			pu.Param = mem.paramsList[i];		
			pu.ParamNameText.text = pu.Param.ShortName;
			pu.ParamValText.text = pu.Param.ToString();
			memUI.paramList.Add(pu);			
		}
		return pos.y + sy;
	}
	void DrawMembersUI()
	{
		ClearParamList();
		Vector2 pos = new Vector2(10, 20);
		float maxHeight = 0;
		for (int j = 0; j < Selected.Params.paramsList.Count; j++)
		{
			AParam item = Selected.Params.paramsList[j];
			MemberUI mu = Instantiate(prefMemberUI);
			mu.Own = Selected;
			mu.GetComponentInChildren<Button>().interactable = uplockParam;
			mu.transform.SetParent(paramPanel.transform);
			mu.GetComponent<RectTransform>().anchoredPosition = pos;
			mu.GeneNameText.text = item.Name;
			mu.SetParam(item);
			paramElList.Add(mu);
			mu.Position = pos;
			float height = DrawParamsUI(mu);
			if(height > maxHeight)
			{
				maxHeight = height;
			}			
			pos.x += mu.Size.x ;
			Vector2 sz = paramPanel.GetComponent<RectTransform>().sizeDelta;
			sz = new Vector2(pos.x, maxHeight + 10);
			paramPanel.GetComponent<RectTransform>().sizeDelta = sz;
		}
	}
}
