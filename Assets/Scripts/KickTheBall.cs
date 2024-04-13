using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickTheBall : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform shootingPoint;
    public GameObject ball;

    [Header("Setting")]
    int totalShoot;
    public float shootCooldown;

    [Header("Shooting")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public float shootForce;
    public float shootUpwardForce;

    AudioManager audioManager;

    [SerializeField] public Animator animator;

    bool readyToShoot;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        readyToShoot = true;
        totalShoot = ShootNumber();
    }

    private void Update()
    {
        GameManager.instance.UpdateBall(totalShoot);

            if (Input.GetKeyDown(shootKey) && readyToShoot && totalShoot > 0)
                 {
                    Shoot();
                    audioManager.PlaySFX(audioManager.ballKick);
                 }
    }

    private void Shoot()
    {
        readyToShoot = false;

        animator.SetBool("shoot", true);

        GameObject projectile = Instantiate(ball, shootingPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalShoot--;

        Invoke(nameof(ResetShoot), shootCooldown);
    }

    private void ResetShoot()
    {
        animator.SetBool("shoot", false);
        readyToShoot = true;
    }

    
    private int ShootNumber()
    {
        int obj = GameManager.instance.ObjectLifeLeft();
        int shootNumber = Mathf.RoundToInt(Random.Range(obj,obj*2));
        return shootNumber;
    }
    
    
}
