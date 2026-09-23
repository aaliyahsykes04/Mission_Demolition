using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public GameObject launchPoint;
    public GameObject prefabProjectile;
    public float velocityMult = 10f; // Adjust the velocity multiplier as needed
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos= launchPoint.transform.position;
    }
    void OnMouseEnter()
    {
        //print ("SlingShot: OnMouseEnter");
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        //print ("SlingShot: OnMouseExit");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        //insantiate a projectile
        projectile =Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

    }

    private void Update()
    {
        if (!aimingMode) return;

        // get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // Convert to 3D world coordinates

        //find the delta (distance) from the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;   
        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;  // Limit the magnitude to the radius of the SphereCollider
        }
        //move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos; 

        if (Input.GetMouseButtonUp(0))
        {
            // The mouse has been released
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous; // Set collision detection mode to Continuous. There are other modes.
            projRB.velocity = -mouseDelta * velocityMult; // Apply the velocity multiplier to the projectile's velocity
            projectile = null; // Clear the reference to the projectile after launching
        }
    }
}
