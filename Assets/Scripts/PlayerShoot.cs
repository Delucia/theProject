using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {


    //Shoting Variables
    public Transform[] gunSpots;
    public GameObject projectile;
    public float[] rates;

    private float[] nextShootTime = new float[4];
    
	
	void Update () 
    {
	    if (networkView.isMine)
	    {
	        // To delay the guns randomly
	        if (Input.GetMouseButtonDown(0))
	        {
	            for (int i = 0; i < gunSpots.Length; i++)
	            {
	                float tempRandom = Random.Range(0f, 1.2f);
	                float n = i + tempRandom;
	                nextShootTime[i] = Time.time + n/10;
	            }
	        }

	        // Keeps shooting as long as mouse is pressed.
	        if (Input.GetMouseButton(0))
	        {
	            Shoot(gunSpots[0], 0);
	            Shoot(gunSpots[1], 1);
	            Shoot(gunSpots[2], 2);
	            Shoot(gunSpots[3], 3);
	        }
	    }
	    else
	    {
	        enabled = false;
	    }
    }
    
    // Takes a transform node and gun index and shoots it.
    void Shoot (Transform spot, int index)
    {
        if (Time.time >= nextShootTime[index])
        {
            Instantiate(projectile, spot.position, spot.rotation);
            
            ShootTimer(rates[index], index);
        }

    }
    
    // Takes the rate of fire value and index for which it is assigned. Adjusts the next shoot time
    void ShootTimer(float freq, int index)
    {
        nextShootTime[index] = Time.time + freq;
    }
}
