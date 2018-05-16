using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AParamValue
{
	public string Icon { get; set; }
	public string ShortName { get; set; }
	public string FullName { get; set; }
	public abstract int Level{ get; set; }
	public abstract void Increase();
	public abstract void Decrease();
}
