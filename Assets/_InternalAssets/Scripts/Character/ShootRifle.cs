using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootRifle : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce,
        upwardForce;

    //Gun stats
    public float timeBetweenShooting,
        spread,
        reloadTime,
        timeBetweenShots;
    public int magazineSize,
        bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft,
        bulletsShot;

    //bools
    bool shooting,
        readyToShoot,
        reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public AudioSource gunshot;
    public AudioClip singleShot;

    public Animator animator;

    //bug fixing :D
    public bool allowInvoke = true;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.CoinToss:
                if (this)
                {
                    Reload();
                }
                break;
        }
    }

    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft + " / " + magazineSize);

        if (
            Input.GetKeyDown(KeyCode.Escape)
            && GameManager.Instance.State == GameState.CharacterShooting
        )
        {
            FinishShooting();
        }
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if (
            readyToShoot
            && shooting
            && !reloading
            && bulletsLeft > 0
            && GameObject.ReferenceEquals(gameObject, GameManager.Instance._character)
            && GameManager.Instance.State == GameState.CharacterShooting
        )
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        gunshot.PlayOneShot(singleShot);
        animator.SetTrigger("Shoot");

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view

        //check if ray hits something
        Vector3 targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet
            .GetComponent<Rigidbody>()
            .AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet
            .GetComponent<Rigidbody>()
            .AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }

    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void FinishShooting()
    {
        GameManager.Instance.FinishCharacterShooting();
    }
}
