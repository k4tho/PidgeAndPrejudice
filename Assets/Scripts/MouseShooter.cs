using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShooter : MonoBehaviour
{
    // wire up the prefab you want to be your projectile
    public GameObject ProjectilePrefab;
    public Pigeon Pigeon;
    public Grandma Grandma;
    public PowerUpLmg PowerUpLmg;
    public PowerDownFakeLmg PowerDownFakeLmg;
    
    // how fast the projectile will move
    public bool isGunSlow = false;
    public bool isGunFast = false;
    public bool isBerserk = false;
    private Coroutine beserkCoroutine;
    private float speed = 5f;


    void Update()
    {
        // get the mouse's position in world space.  we need this to send the projectile toward the mouse.
        Vector3 mouseWorldPosition = GetMousePositionInWorldSpace();
        
        if (isBerserk == true)
        {
            GameObject projectileObject = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

            Vector3 projectileDirection3D = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f) - transform.position;
            
            projectileObject.GetComponent<Rigidbody>().velocity += speed * projectileDirection3D;

            Pigeon.FaceCorrectDirection(projectileDirection3D);
        }
        
        // if we left-click the mouse
        else if (Input.GetButtonDown("Fire1"))
        {
            float newSpeed = speed;

            if (CheckForGunSpeed() == true)
                newSpeed = ApplyGunChanges(speed);

            // create a projectile object at the position of whatever object this script is attached to
            // (probably the object that's doing the shooting, like the player or an enemy)
            GameObject projectileObject = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

            // figure out the x/y direction the projectile should move in
            Vector3 projectileDirection3D = GetProjectileDirection(mouseWorldPosition);

            // add velocity to the projectile's rigidbody in a direction and at a speed
            projectileObject.GetComponent<Rigidbody>().velocity += newSpeed * projectileDirection3D;

            Pigeon.FaceCorrectDirection(projectileDirection3D);
        }
    }

    private Vector3 GetProjectileDirection(Vector3 mouseWorldPosition)
    {
        // the direction is the mouse's location minus the shooter object's location
        // the z value is there to offset the camera's z value.  
        Vector3 projectileDirection3D = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 
            Camera.main.transform.position.z * -1) - transform.position;

        return projectileDirection3D;
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
            Camera.main.transform.position.z * -1);
        
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private bool CheckForGunSpeed()
    {
        if (isGunFast == true)
            return true;
        else if (isGunSlow == true)
            return true;
        else
            return false;
    }
    
    private float ApplyGunChanges(float speed)
    {
        float newSpeed = speed;
        
        if (isGunFast == true)
            newSpeed = PowerUpLmg.ApplyFastGunSpeed(speed);
        else if (isGunSlow == true)
            newSpeed = PowerDownFakeLmg.ApplySlowGunSpeed(speed);

        return newSpeed;
    }

    public void GoBerserk()
    {
        Grandma.MakeGrandmaInvincible();
        beserkCoroutine = StartCoroutine(RandomFiringTime());
    }
    
    private IEnumerator RandomFiringTime()
    {
        isBerserk = true;
        yield return new WaitForSeconds(GameParameters.randomFiringTimer);
        ResetRandomFiring();
    }

    public void ResetRandomFiring()
    {
        Grandma.MakeGrandmaDestructible();
        isBerserk = false;
        beserkCoroutine = null;
    }

    public void ResetGunSpeed()
    {

    }
}
