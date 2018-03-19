using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunmanagement : MonoBehaviour {

	[Header("this script determines if the hands are free or not")]
	public bool lefthandfree = true;
	public bool righthandfree = true;
	public GameObject lefthand;
	public GameObject righthand;
	private Transform lefthandt;
	private Transform righthandt;
	private float pickupcd;
	private float pickupt = 0.01f;
	private  GameObject gunToEquip;
    private Transform weaponTransform;
    private  GameObject collidedpickup;
	// Use this for initialization
	void Start () {
		
		lefthandt = lefthand.transform;

		righthandt = righthand.transform;

       
    }
	
	// Update is called once per frame
	void Update () {
		if (pickupcd < 0.5) {
			pickupcd += Time.deltaTime * 10;

		}
	}
	void OnTriggerStay(Collider other)
	{
        //Debug.Log("collider00");


		if (other.tag == "gunpickup" && Input.GetButtonDown("Interact") && (lefthandfree == true || righthandfree == true)) {

            //referemces
            gunToEquip = other.gameObject;
            playershooting gunn = gunToEquip.GetComponent<playershooting>();
            
            weaponTransform = gunn.customtransform;
            Transform newtransform = gunToEquip.GetComponent<Transform>();
            Transform transfurm = gameObject.GetComponent<Transform>();
            transfurm = newtransform;
            Rigidbody rigidbodyofgun = gunToEquip.GetComponent<Rigidbody>();
            rigidbodyofgun.isKinematic = true;

            if (lefthandfree == true &&	righthandfree == true && pickupcd > pickupt) {
            	Debug.Log (" all hands were free and righthand is now occupied ");

                //making the gun a child of the character
                gunToEquip.transform.parent = righthand.transform;
            
                //disabling the collider of the weapon
            BoxCollider collider = gunToEquip.GetComponent<BoxCollider>();
            collider.enabled = !collider.enabled;

                newtransform.localPosition = weaponTransform.localPosition;
                newtransform.localRotation = weaponTransform.localRotation;
                newtransform.localScale = weaponTransform.localScale;

                righthandfree = false;
                	pickupcd = 0f;
                //OLD SYSTEM
                //	var newgun = (GameObject)Instantiate (
                //		gunTI,
                //		weaponTransform.position,
                //		weaponTransform.rotation);
                //	newgun.transform.parent = righthand.transform;
                //	var instantiatedweapon = newgun.GetComponent<playershooting> ();
                //		instantiatedweapon.ammo = pickup.bulletcount;
                //	Destroy(other.gameObject);
                //	righthandfree = false;
                //	pickupcd = 0f;
                //OLD SYSTEM
            } 
                	else if (righthandfree == false && lefthandfree == true && pickupcd > pickupt) {

                    Debug.Log ("left hand was free and is occupied now");

                    gunToEquip.transform.parent = lefthand.transform;

                    BoxCollider collider2 = gunToEquip.GetComponent<BoxCollider>();
                    collider2.enabled = !collider2.enabled;

                    newtransform.localPosition = weaponTransform.localPosition;
                    newtransform.localRotation = weaponTransform.localRotation;
                    newtransform.localScale = weaponTransform.localScale;

                    lefthandfree = false;
                    pickupcd = 0f;

                }
                	else if (righthandfree == true && lefthandfree == false && pickupcd > pickupt) {

                    Debug.Log ("right hand was free and is occupied now");

                    gunToEquip.transform.parent = righthand.transform;

                BoxCollider collider3 = gunToEquip.GetComponent<BoxCollider>();
                collider3.enabled = !collider3.enabled;

                newtransform.localPosition = weaponTransform.localPosition;
                newtransform.localRotation = weaponTransform.localRotation;
                newtransform.localScale = weaponTransform.localScale;

                righthandfree = false;
                pickupcd = 0f;

            }
        }
	
	
	
	}
}
