using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBody
{	
	Creature Own { get; set; }
	void Redraw();
}
