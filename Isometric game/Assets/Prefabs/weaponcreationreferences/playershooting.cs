using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour {

	[Header("shoot speed, lower for faster shooting")]
	public float shootinterval;
	//timer is used for shooting intervals
	float shootingintervaltimer;
    float gunthrowtimer;
    [Header("drag the associated weapon's prop/pickup")]
	public GameObject testgunpickup;
	[Header("preferred bullet prefab")]
	public GameObject bulletp;
	[Header("transform of barrel")]
	public Transform bulletSpawn;
	[Header("speed of the bullet")]
	public float BulletForce;
	[Header("speed of the thrown gun")]
	public float gunthrowforce;
	[Header("maximum ammo")]
	public int  ammocap = 10;
	[Header("true equals automatic fire")]
	public bool autofire;
	[Header("muzzleflash of this gun, has to be a child of this gameobject.")]
	public GameObject muzzleflash;
	[Header("count of ammo that is currently in the weapon")]
	public int ammo;
    public Transform customtransform;
    //a reference to the script of the pickup gameobject so we can tell the pickup our hands 
	private gunmanagement gunmanager;
	private string whichgun;
	private ParticleSystem muzzleparticle;
    private bool throwtimerenabler;
    private Rigidbody rb;
    void Awake (){


		//finds the player gameobject to change the state of hands' state after running out of ammo
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		gunmanager = player.GetComponent<gunmanagement> ();

		//getting the particlesystem component from muzzleflash gameobject
		muzzleparticle = muzzleflash.GetComponent<ParticleSystem> ();
        rb = GetComponent<Rigidbody>();
        

    }
    void Start()
    {
        
        //when this weapon is instantiated, tell the gunmanager the hand is not free
        if (transform.parent)
        {
            if (transform.parent.tag == "leftgun")
            {
                gunmanager.lefthandfree = false;
                whichgun = "leftgun";
            }
            else if (transform.parent.tag == "rightgun")
            {
                gunmanager.righthandfree = false;
                whichgun = "rightgun";
            }
            rb.isKinematic = false;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        throwcooldown();

        if (transform.parent)
        {
            

            firingtehgun();

            if (Input.GetButtonDown("Throw Left Gun") && transform.parent.tag == "leftgun")
            {
                gunthrow();

            }
            else if (Input.GetButtonDown("Throw Right Gun") && transform.parent.tag == "rightgun")
            {
                gunthrow();
            }
        
        //		if (ammo == 0) {
        //		play a clicking noise
        //		}

        //gun throwing code
         
        
    }
    }
    void firingtehgun() {
        shootingintervaltimer += Time.deltaTime;
        if (autofire == false )
        {
            //leftmouse button & leftgun & interval GetButtonDown = semi , GetButton = full
            if (Input.GetButtonDown("Fire1") && transform.parent.tag == "leftgun" && shootinterval < shootingintervaltimer && ammo > 0)
            {
                var bullet = (GameObject)Instantiate(
                                bulletp,
                                bulletSpawn.position,
                                bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletForce;
                ammo -= 1;
                shootingintervaltimer = 0f;
                muzzleparticle.Play();

            }
            //the same thing for the right hand 
            else if (Input.GetButtonDown("Fire2") && transform.parent.tag == "rightgun" && shootinterval < shootingintervaltimer && ammo > 0)
            {
                var bullet = (GameObject)Instantiate(
                                 bulletp,
                                 bulletSpawn.position,
                                 bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletForce;
                ammo = ammo - 1;
                shootingintervaltimer = 0f;
                muzzleparticle.Play();
            }
        }
        else if (autofire == true )
        {
            //leftmouse button & leftgun & interval GetButtonDown = semi , GetButton = full
            if (Input.GetButton("Fire1") && transform.parent.tag == "leftgun" && shootinterval < shootingintervaltimer && ammo > 0)
            {
                var bullet = (GameObject)Instantiate(
                    bulletp,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletForce;
                ammo -= 1;
                shootingintervaltimer = 0f;
                muzzleparticle.Play();
            }
            //the same thing for the right hand 
            else if (Input.GetButton("Fire2") && transform.parent.tag == "rightgun" && shootinterval < shootingintervaltimer && ammo > 0)
            {
                var bullet = (GameObject)Instantiate(
                    bulletp,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletForce;
                ammo = ammo - 1;
                shootingintervaltimer = 0f;
                muzzleparticle.Play();
            }
        }

    }
    void throwcooldown() {

        if (throwtimerenabler == true)
        {
            gunthrowtimer += Time.deltaTime;
        }

        if (gunthrowtimer >= 0.5f)
        {
            BoxCollider actualcollider = gameObject.GetComponent<BoxCollider>();
            actualcollider.enabled = true;
            throwtimerenabler = false;
            gunthrowtimer = 0f;
        }
        //Debug.Log(gunthrowtimer);
    }



    void gunthrow () {


        gunthrowtimer = 0f;
        //tells the gunmanagement script the hand is free and ready for picking up objects again
		if (transform.parent.tag == "rightgun") {
			gunmanager.righthandfree = true;

        } 
		else {
			gunmanager.lefthandfree = true;
		
		}
        transform.parent = null;        
        throwtimerenabler = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * gunthrowforce);

    }

	



}
