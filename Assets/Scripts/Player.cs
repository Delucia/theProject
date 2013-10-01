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

    // Network
    private Quaternion lastRotation;
    private Vector3 lastPosition;
    

	void Start () 
    {
	
	}
	
	
	void Update ()
	{
	    if (networkView.isMine)
	    {
	        //// ------  Movement  ---------- /////////
	        targetSpeed.x = Input.GetAxis("Horizontal");
	        targetSpeed.y = Input.GetAxis("Vertical");

	        if (targetSpeed.sqrMagnitude > 1)
	            targetSpeed.Normalize();

	        targetSpeed *= speed*Time.deltaTime;

	        currentSpeed.x = Accelerate(currentSpeed.x, targetSpeed.x, acceleration);
	        currentSpeed.y = Accelerate(currentSpeed.y, targetSpeed.y, acceleration);
	        transform.Translate(new Vector3(currentSpeed.x, 0, currentSpeed.y));

            // Update in network
            if (Vector3.Distance(transform.position, lastPosition) >= 0.1f)
            {
                // Capture the player's position before the RPC is fired off and use this 
                // to determine if the player has moved in the if statement above.
                lastPosition = transform.position;
                networkView.RPC("UpdateMovement", RPCMode.OthersBuffered, transform.position, transform.rotation);
            }

	        //---------------------------------------------------


	        //// --------- Head Look At Mouse Cursor ------ /////////
	        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;

	        if (Physics.Raycast(ray, out hit))
	        {
	            DesiredLookAt = Quaternion.LookRotation(hit.point - transform.position);
	            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, DesiredLookAt, lookSpeed*Time.deltaTime);
	            head.transform.eulerAngles = new Vector3(0, head.transform.eulerAngles.y, 0);
	        }

            // Update in network
	        if (Quaternion.Angle(head.transform.rotation, lastRotation) >= 1)
	        {
                lastRotation = head.transform.rotation;
                networkView.RPC("UpdateHeadRotation", RPCMode.OthersBuffered, head.transform.rotation);
	        }
	            
	        //---------------------------------------------
	    }
	    else
	    {
	        enabled = false;
	    }
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

    [RPC]
    void UpdateHeadRotation(Quaternion newRot)
    {
        head.transform.rotation = newRot;
    }


    [RPC]
    void UpdateMovement(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.rotation = newRot;
    }
}
