﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBar : MonoBehaviour {

	void Update () {		
		transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
	}
}
