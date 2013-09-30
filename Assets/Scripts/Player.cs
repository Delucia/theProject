//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;

using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // Movement Variables
    public float speed;
    public float acceleration;
    private Vector2 currentSpeed;
    private Vector2 targetSpeed;

    //Head LookAt Variables
    public GameObject head;
    public float lookSpeed;
    private Quaternion DesiredLookAt;
    private Ray ray;

    

	void Start () 
    {
	
	}
	
	
	void Update ()
	{
	    //// ------  Movement  ---------- /////////
        targetSpeed.x = Input.GetAxis("Horizontal");
        targetSpeed.y = Input.GetAxis("Vertical");
	    
        if(targetSpeed.sqrMagnitude > 1)
            targetSpeed.Normalize();

        targetSpeed *= speed * Time.deltaTime;
        
	    currentSpeed.x = Accelerate(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.y = Accelerate(currentSpeed.y, targetSpeed.y, acceleration);
        transform.Translate(new Vector3(currentSpeed.x, 0, currentSpeed.y));
        //---------------------------------------------------
        

        //// --------- Head Look At Mouse Cursor ------ /////////
	    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;

	    if (Physics.Raycast(ray, out hit))
	    {
	        DesiredLookAt = Quaternion.LookRotation(hit.point - transform.position);
	        head.transform.rotation = Quaternion.Lerp(head.transform.rotation, DesiredLookAt, lookSpeed * Time.deltaTime);
            head.transform.eulerAngles = new Vector3(0, head.transform.eulerAngles.y, 0);
	    }
        //---------------------------------------------
        
	}
    
    // Increments the speed towards desired speed by acceleration amount
    float Accelerate(float n, float target, float speed)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += speed*Time.deltaTime*dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }


}
