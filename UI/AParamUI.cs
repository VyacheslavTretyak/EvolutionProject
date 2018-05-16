using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AParamUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	protected Vector2 size;
	public Vector2 Size { get { return size; } }	
	public Vector2 Position { get; set; }
	public Creature Own { get; set; }
	protected SelectedInfo selInfo;
	protected bool showLabel = false;
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		showLabel = true;		
	}
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		showLabel = false;
	}	
}
