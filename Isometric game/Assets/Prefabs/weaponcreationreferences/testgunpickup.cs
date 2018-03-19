using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testgunpickup : MonoBehaviour {
	[Header("ammount of ammo in the pickup weapon")]
	public  int bulletcount;
	[Header("gun to instantiate")]
	public GameObject gun;

   


	void Awake (){
        gameObject.GetComponent<MeshFilter>().sharedMesh = gun.GetComponent<MeshFilter>().sharedMesh;
        transform.localScale = gun.transform.localScale;
	
	
	}

	// Use this for initialization
	void Start () {







	
	}
	
	// Update is called once per frame
	void Update () {



		}




}




