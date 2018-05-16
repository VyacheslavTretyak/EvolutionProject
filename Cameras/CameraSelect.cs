using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CameraSelect : MonoBehaviour
{
	float lastTime = 0;
	float freqTime = 0.5f;
	Generator gen;
	bool showLabel = false;
	string str;
	SelectedInfo panel;
	private void Start()
	{
		panel = FindObjectOfType<SelectedInfo>();
		gen = FindObjectOfType<Generator>();
	}
	private void OnGUI()
	{
		if (showLabel)
		{
			Rect r = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 200, 50);
			GUI.Box(r, str);
		}
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			OnMove(hit.collider.GetComponent<Creature>());
		}
		if (Input.GetMouseButton(0))
		{
			if (GetComponent<Mouse>().IsStrateryMode)
			{
				return;
			}
			if (Physics.Raycast(ray, out hit))
			{
				OnLeftClick(hit);				
			}
		}
		if (Input.GetMouseButton(1))
		{
			if (Physics.Raycast(ray, out hit))
			{
				OnRightClick(hit);
			}
		}
	}
	void OnMove(Creature creature)
	{
		if (creature)
		{
			str = $"{creature.ToString()}[{creature.Gender}]\n{creature.Behaviors.Action}\n{creature.CurrentTile.HeatT},{creature.CurrentTile.MoinstureT}:{creature.EnvironmentValue}";
			showLabel = true;
		}
		else
		{
			showLabel = false;
		}
	}
	void OnRightClick(RaycastHit hit)
	{
		if (panel.Selected)
		{
			panel.Selected.Behaviors.StopAction();
			panel.Selected.Tr.position = hit.point + new Vector3(0, 0.1f, 0);
			Vector3 l = Camera.main.transform.position;
			panel.Selected.Tr.LookAt(new Vector3(l.x, panel.Selected.Tr.position.y, l.z));
		}
		else
		{
			if (Time.time - lastTime > freqTime)
			{
				Creature creature = Instantiate(gen.prefabCreature);
				creature.Tr.position = hit.point;
				creature.Init(new Params(creature));

				creature.Conditions.GetContition<Hunger>().Amount = 0.3f;
				creature.Conditions.GetContition<Energy>().Amount = 1f;
				creature.Conditions.GetContition<Age>().Amount = 0.5f;
				creature.Conditions.GetContition<Health>().Amount = 0.3f;
				creature.Conditions.GetContition<Pregnant>().Amount = 0;
				creature.SetGrow();

				Vector3 l = Camera.main.transform.position;
				creature.Tr.LookAt(new Vector3(l.x, creature.Tr.position.y, l.z));
				lastTime = Time.time;
			}
		}
	}
	void OnLeftClick(RaycastHit hit)
	{
		if (hit.collider != null)
		{
			panel.SetSelected(hit.collider.gameObject.GetComponent<Creature>());
		}
	}
}
