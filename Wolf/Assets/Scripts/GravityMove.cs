using UnityEngine;
using System.Collections;

public class GravityMove : MonoBehaviour {

	public float gravitySpeed; //500
	public LayerMask RayCastLayerMask;	// Level
	public float movementSpeed; //2
	public float rotationSpeedGrounded; //10
	public float rotationSpeedFly; //1
		
	private float distForward;
	private float distDown;
	private float distBack;
	
	private float rotationSpeed;
	
	public bool isGrounded;
	
	private bool move;

	void Start(){

		isGrounded = true;
		rotationSpeed = rotationSpeedGrounded;	

	}

	// Update is called once per frame
	void FixedUpdate () {
		
		//control
		move = false;
		
		if ((Input.GetAxis("Vertical"))>0)					
		{
			//Forward			
			transform.position += transform.forward * movementSpeed * Time.deltaTime;
			move = true;		
		}
		else if ((Input.GetAxis("Vertical"))<0)
		{			
			//Back			
			transform.position -= transform.forward * movementSpeed * Time.deltaTime;
			move = true;
		}
		
		if ((Input.GetAxis("Horizontal"))<0)					
		{					
			//Left		
			transform.RotateAround (transform.up, Input.GetAxis("Horizontal") * 3 * Time.deltaTime);			
			move = true;			
		}
		else if ((Input.GetAxis("Horizontal"))>0)					
		{		
			//Right			
			transform.RotateAround (transform.up, Input.GetAxis("Horizontal") * 3 * Time.deltaTime);
			move = true;
		}
				
		//stick
		distForward = Mathf.Infinity;
		RaycastHit hitForward;
		if (Physics.SphereCast(transform.position, 0.25f, -transform.up + transform.forward, out hitForward, 5, RayCastLayerMask))
		{
			distForward = hitForward.distance;				
		}
		distDown = Mathf.Infinity;
		RaycastHit hitDown;
		if (Physics.SphereCast(transform.position, 0.25f, -transform.up, out hitDown, 5, RayCastLayerMask))
		{
			distDown = hitDown.distance;				
		}
		distBack = Mathf.Infinity;
		RaycastHit hitBack;
		if (Physics.SphereCast(transform.position, 0.25f, -transform.up + -transform.forward, out hitBack, 5, RayCastLayerMask))
		{
			distBack = hitBack.distance;				
		}
		
		if (distForward < distDown && distForward < distBack)
		{			
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitForward.normal), hitForward.normal), Time.deltaTime * rotationSpeed);
		}
		else if (distDown < distForward && distDown < distBack)
		{				
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal), Time.deltaTime * rotationSpeed);
		}
		else if (distBack < distForward && distBack < distDown)
		{				
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitBack.normal), hitBack.normal), Time.deltaTime * rotationSpeed);
		}				
		
		//gravity
		GetComponent<Rigidbody>().AddForce(-transform.up * Time.deltaTime * gravitySpeed); 
			
	}

	void OnCollisionEnter(Collision col) 
	{
		//if character collide with level
		if (((1<<col.gameObject.layer) & RayCastLayerMask) != 0)		
		{			
			isGrounded = true;
			rotationSpeed = rotationSpeedGrounded;
		}	
		
		//stick to animated platform
		if (col.gameObject.tag == "Platform") transform.parent = col.transform;		
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Platform") transform.parent = null;
	}

}
