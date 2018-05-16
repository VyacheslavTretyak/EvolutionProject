using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderPic : MonoBehaviour {
	public Texture boyPic;
	public Texture girlPic;
	RawImage img;
	void Awake()
	{
		img = GetComponent<RawImage>();		
	}
	public void SetPicture(Creature cr)
	{
		if(cr.Gender == GenderType.Boy)
		{
			img.texture = boyPic;
		}
		else
		{
			img.texture = girlPic;
		}
	}

}
