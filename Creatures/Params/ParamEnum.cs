using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamEnum : AParamValue
{
	public List<string> values;
	int value = 0;
	public override int Level {
		get { return value; }
		set
		{
			this.value = value;
			if(this.value >= values.Count)
			{
				this.value = values.Count - 1;
			}
			if(this.value < 0)
			{
				this.value = 0;
			}
		}
	}
	public override void Increase()
	{
		Level++;		
	}
	public override void Decrease()
	{
		Level--;
	}
	public override string ToString()
	{
		return values[Level];
	}
}
