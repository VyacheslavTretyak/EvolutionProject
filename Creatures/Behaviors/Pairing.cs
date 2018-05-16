using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pairing : AAction
{
	public Creature Pair { get; set; }

	float pairingTime = 5f;//seconds
	float time = 0;
	ParticleSystem particle;		
	public Pairing(Creature own)
	{
		Own = own;
		particle = Own.GetComponentInChildren<ParticleSystem>();
	}
	public override void Start()
	{
		if (Own.Gender == GenderType.Boy)
		{
			particle.Play();
			Pair = Own.Behaviors.GetBehavior<FindPair>().Pair;
			Pair.Behaviors.GetBehavior<Pairing>().Pair = Own;
			Pair.Behaviors.StartAction<Pairing>();
		}
		Own.RotateTo(Pair.Tr.position);
		time = 0;
	}
	protected override void GeneUpdate()
	{
		if(time == 0)
		{
			time = Time.time;
			if (Vector3.Distance(Own.Tr.position, Pair.Tr.position) > Own.Params.BodyParam.MinDistanceToAny)
			{
				Own.Behaviors.StopAction();
			}
		}
		else if(Time.time - time > pairingTime)
		{	
			if (Own.Gender == GenderType.Girl)
			{
				Own.Behaviors.GetBehavior<Reproduction>().CreateBaby(Pair);				
			}					
			Own.Behaviors.StopAction();
		}	
	}
	public override void Stop()
	{
		Pair = null;
		particle.Stop();
	}
}
