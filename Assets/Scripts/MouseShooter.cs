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
    private Coroutine gunSprayCoroutine;

    void start()
    {
        gunSprayCoroutine = null;
    }

    void Update()
    {
        float newSpeed = GameParameters.bulletAvgSpeed;
        float newGunSprayTimer = GameParameters.normalGunSprayTimer;

        if (CheckForGunSpeed() == true)
        {
            newSpeed = ApplyGunSpeedChanges();
            newGunSprayTimer = ApplyGunSprayChanges();
        }


        // get the mouse's position in world space.  we need this to send the projectile toward the mouse.
        Vector3 mouseWorldPosition = GetMousePositionInWorldSpace();

        if (isBerserk == true && gunSprayCoroutine == null)
        {
            StartGunSprayTimer(GameParameters.fastGunSprayTimer);

            GameObject projectileObject = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

            Vector3 projectileDirection3D = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f) - transform.position;

            projectileObject.GetComponent<Rigidbody>().velocity += GameParameters.fastBulletSpeed * projectileDirection3D;

            Pigeon.FaceCorrectDirection(projectileDirection3D);
        }

        // if we left-click the mouse
        else if (Input.GetButtonDown("Fire1"))
        {
            if (gunSprayCoroutine == null)
            {
                StartGunSprayTimer(newGunSprayTimer);

                // create a projectile object at the position of whatever object this script is attached to
                // (probably the object that's doing the shooting, like the player or an enemy)
                GameObject projectileObject = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

                // figure out the x/y direction the projectile should move in
                Vector3 projectileDirection3D = GetProjectileDirection(mouseWorldPosition);

                // add velocity to the projectile's rigidbody in a direction and at a speed
                projectileObject.GetComponent<Rigidbody>().velocity += newSpeed * projectileDirection3D;
                Debug.Log(projectileDirection3D);
                Debug.Log(newSpeed * projectileDirection3D);
                //projectileObject.GetComponent<Rigidbody>().AddRelativeForce(newSpeed * projectileDirection3D);

                Pigeon.FaceCorrectDirection(projectileDirection3D);
            }
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
    
    private float ApplyGunSpeedChanges()
    {
        float newSpeed = 0f;

        if (isGunFast == true)
            newSpeed = PowerUpLmg.ApplyFastGunSpeed();
        else if (isGunSlow == true)
            newSpeed = PowerDownFakeLmg.ApplySlowGunSpeed();

        return newSpeed;
    }

    private float ApplyGunSprayChanges()
    {
        float newGunSpray = 0f;

        if (isGunFast == true)
            newGunSpray = PowerUpLmg.ApplyFastGunSpray();
        else if (isGunSlow == true)
            newGunSpray = PowerDownFakeLmg.ApplySlowGunSpray();

        return newGunSpray;
    }

    public void GoBerserk()
    {
        Grandma.MakeGrandmaInvincible();
        StartCoroutine(RandomFiringTime());
    }
    
    private IEnumerator RandomFiringTime()
    {
        isBerserk = true;
        yield return new WaitForSeconds(GameParameters.randomFiringTimer);
        ResetRandomFiring();
    }

    public void ResetRandomFiring()
    {
        isBerserk = false;
        Grandma.MakeGrandmaDestructible();
    }

    public void ResetGunSpeed()
    {

    }

    private void StartGunSprayTimer(float gunSprayTimer)
    {
        gunSprayCoroutine = StartCoroutine(WaitForNextSpray(gunSprayTimer));
    }

    private IEnumerator WaitForNextSpray(float gunSprayTimer)
    {
        yield return new WaitForSeconds(gunSprayTimer);
        gunSprayCoroutine = null;
    }
}
