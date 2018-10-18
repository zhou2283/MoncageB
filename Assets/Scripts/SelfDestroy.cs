using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
	[SerializeField] private float delay = 0f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
