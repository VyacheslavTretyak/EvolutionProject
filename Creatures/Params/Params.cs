using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Params:AGene
{	
	public List<AParam> paramsList;
	public BodyParam BodyParam { get; set; }
	public NoseParam NoseParam { get; set; }
	public JawsParam JawsParam { get; set; }
	public EyesParam EyesParam { get; set; }
	public LegsParam LegsParam { get; set; }
	public EarsParam EarsParam { get; set; }
	public WingsParam WingsParam { get; set; }
	public ShellParam ShellParam { get; set; }
	public ThornsParam ThornsParam { get; set; }	
	public Params()
	{		
		Own = null;
		paramsList = new List<AParam>();
		BodyParam = new BodyParam();		
		paramsList.Add(BodyParam);
		NoseParam = new NoseParam
		{
			IsActive = true
		};
		paramsList.Add(NoseParam);
		JawsParam = new JawsParam
		{
			IsActive = true
		};
		paramsList.Add(JawsParam);
		EyesParam = new EyesParam();
		paramsList.Add(EyesParam);
		LegsParam = new LegsParam();
		paramsList.Add(LegsParam);
		EarsParam = new EarsParam();
		paramsList.Add(EarsParam);
		WingsParam = new WingsParam();
		paramsList.Add(WingsParam);
		ShellParam = new ShellParam();
		paramsList.Add(ShellParam);
		ThornsParam = new ThornsParam();
		paramsList.Add(ThornsParam);
	}
	public Params(Creature own):this()
	{
		Own = own;
		SetOwn(Own);
	}
	public void SetOwn(Creature own)
	{
		foreach (AParam par in paramsList)
		{
			par.Own = own;			
		}
	}
	public T GetParam<T>()
	{
		foreach (AParam item in paramsList)
		{
			if(item is T)
			{
				return (T)(object)item;
			}
		}
		return default(T);
	}	
}

