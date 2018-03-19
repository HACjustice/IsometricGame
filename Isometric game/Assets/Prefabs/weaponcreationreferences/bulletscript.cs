using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour {

	public int damage;
	float timer;
	public float bulletlife;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= bulletlife) {
			Debug.Log ("bullet dissappeared"); 
			Destroy  (gameObject);
		
		}



    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Projectile") {
            Destroy(gameObject);
        }

}



}
