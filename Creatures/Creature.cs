using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenderType
{
	Boy,
	Girl
}
public class Creature : MonoBehaviour
{
	public static int globalID = 1;
	public static GenderType nextGender = GenderType.Girl;

	public List<MonoBehaviour> bodyMembersPref;

	public Generator Map { get; private set; }
	public Transform Tr { get; private set; }
	public Rigidbody Rb { get; private set; }
	public BoxCollider BoxCol { get; private set; }
	public Finder Finder { get; private set; }


	public Members Members { get; private set; }
	public Conditions Conditions { get; private set; }
	public Behaviors Behaviors { get; private set; }
	public Params Params{ get; set; }

	public int ID { get; set; }
	public int Generation { get; set; } = 1;
	public int Kind { get; set; }
	public GenderType Gender { get; set; } = GenderType.Boy;
	public bool IsAlive { get; set; } = true;
	public bool IsGrounded { get; set; } = false;
	public Tile CurrentTile { get; set; }
	public float EnvironmentValue { get; set; }
	public PregnantPanel PregnantPanel { get; set; }
	void Awake()
	{
		Map = FindObjectOfType<Generator>();
		Tr = transform;
		Tr.SetParent(Map.Transform);		
		Finder = new Finder(this);

		ID = globalID++;
		name = "Creature " + ID;		
		Gender = nextGender = nextGender == GenderType.Boy ? GenderType.Girl : GenderType.Boy;		

		Rb = GetComponent<Rigidbody>();
		PregnantPanel = GetComponentInChildren<PregnantPanel>();

		BoxCol = GetComponent<BoxCollider>();
	}
	private float GetEnvironmentValue()
	{
		int t = Mathf.Abs(((int)CurrentTile.HeatT) - Params.BodyParam.Temperature.Level);
		int m = Mathf.Abs(((int)CurrentTile.MoinstureT) - Params.BodyParam.Moinsture.Level);
		float e = t + m - (Params.BodyParam.EnvironmentResist - 1f);
		if (e > 1)
		{
			return e + 1;
		}
		return 1f;
	}
	private void SetKind()
	{
		int kind = 0;
		foreach (AParam item in Params.paramsList)
		{
			kind |= item.IsActive ? 1 : 0;
			kind <<= 1;
		}
		Kind = kind;
	}
	public int CountMembersOfKind(int otherKind)
	{
		int res = Kind ^ otherKind;
		int count = 0;
		while(res != 0)
		{
			count += res & 1;
			res >>= 1;
		}
		return count;
	}

	public void Init(Params prms)
	{
		Params = prms;
		Members = new Members(this);
		Conditions = new Conditions(this);
		Behaviors = new Behaviors(this);
		SetKind();
	}
	void FixedUpdate()
	{		
		if (IsAlive)
		{
			CurrentTile = Map[(int)Tr.position.x, (int)Tr.position.z];
			EnvironmentValue = GetEnvironmentValue();
			Conditions.Update();
			Behaviors.Update();
		}
	}
	public void NoseSense(Collider other)
	{
		if (Params.NoseParam.IsActive)
		{
			if (Behaviors.Action is FindPair)
			{
				Creature creature = other.GetComponent<Creature>();
				if (creature != null)
				{
					Behaviors.GetBehavior<FindPair>().TriggerInvoke(creature);
				}
			}
		}
	}
	public void EyesSense(Collider other)
	{
		//TODO eyes sense
	}
	public void EarsSense(Collider other)
	{
		//TODO ears sense
	}
	public void SetGrow()
	{
		float s = Params.BodyParam.GetSize();
		Rb.mass = s;
		Tr.localScale = new Vector3(s, s, s);		
	}		
	public override string ToString()
	{
		return name;
	}
	public void RotateTo(Vector3 target)
	{
		Tr.LookAt(target, Vector3.up);
	}
	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.GetComponent<TerrainChunk>() != null)
		{
			IsGrounded = false;
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.GetComponent<TerrainChunk>() != null)
		{
			IsGrounded = true;
		}
		if (collision.gameObject.GetComponent<Creature>() != null)
		{
			Vector3 f = Tr.TransformDirection(Vector3.forward);
			Vector3 r = Tr.TransformDirection(Vector3.right);
			Vector3 dir = collision.gameObject.transform.position - Tr.position;
			float dotF = Vector3.Dot(f, dir);
			float dotR = Vector3.Dot(r, dir);
			if (dotF < 0 && dotR < 0)
			{
				//Rb.velocity = (Tr.right) * Params.BodyParam.GetSpeed();
			}
			else if (dotF < 0 && dotR > 0)
			{
				//Rb.velocity = (-Tr.right) * Params.BodyParam.GetSpeed();
			}
			if (dotF > 0 && dotR < 0)
			{
				//Rb.velocity = (Tr.right) * Params.BodyParam.GetSpeed();
			}
			if (dotF > 0 && dotR > 0)
			{
				//Rb.velocity = (-Tr.right) * Params.BodyParam.GetSpeed();
			}
		}
	}
}