using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_OBJ : MonoBehaviour
{
	public Rigidbody rb;
	public BoxCollider coll;
	public Transform Player, objContainer, MainCamera;
	
	public float pickUpRange;
	public float dropForwardForce, dropUpwardForce;
	
	public bool equipped;
	public static bool slotFull;
	
    // Start is called before the first frame update
    void Start()
    {
        if(!equipped)
        {
        	rb.isKinematic = false;
        	coll.isTrigger = false;
        }
        
        if(equipped)
        {
        	rb.isKinematic = true;
        	coll.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();
        
        
        if(equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }
    
    
    
    void PickUp(){
    	equipped = true;
    	slotFull = true;
    	
    	transform.SetParent(ObjContainer);
    	transform.localPosition = Vector3.zero;
    	transform.localRotation = Quaternion.Euler(Vector3.zero);
    	transform.localScale = Vector3.one;
    	
    	rb.isKinematic = true;
    	coll.isTrigger = true;
    	gunScript.Enabled = true;
    }
    
    void Drop(){
    	equipped = false;
    	slotFull = false;
    	
    	Trasform.SetParent(null);
    	
    	rb.isKinematic = false;
    	coll.isTrigger = false;
    	
    	rb.velocity = Player.GetComponent<Rigidbody>().velocity;
    	rb.AddForce(MainCamera.forward * dropForwardForce, ForceMode.Impulse);
    	rb.AddForce(MainCamera.up * dropUpwardForce, ForceMode.Impulse);
    	
    	float random = Random.Range(-1f, 1f);
    	rb.AddTorque(new Vector3(random, random, random)*10);
    }
}
