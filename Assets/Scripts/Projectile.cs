using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{


    public float speed;
    

	void Start () 
    {
	
	}
	
	void Update () 
    {
	    transform.Translate(new Vector3(0, 0, speed*Time.deltaTime));
	}

    
}
