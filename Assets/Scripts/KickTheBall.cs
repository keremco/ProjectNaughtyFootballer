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
    int obj;
    public float shootCooldown;

    [Header("Shooting")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public float shootForce;
    public float shootUpwardForce;

    int escapePress = 0;
    int hitCount;
    int minBallCount = 0;

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
        Invoke(nameof(ShootNumber), 0.01f);
    }

    private void Update()
    {
        hitCount = GameManager.instance.ObjectLifeLeft();

        if (Input.GetKeyDown("escape") && escapePress == 0)
        {
            escapePress = 1;
        }
        else if (Input.GetKeyDown("escape") && escapePress == 1)
        {
            escapePress = 0;
        }

        GameManager.instance.UpdateBall(totalShoot);
        if (Input.GetKeyDown(shootKey) && escapePress == 0 && (hitCount > 0) && readyToShoot && totalShoot > 0)
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

        Invoke(nameof(ResetShoot), shootCooldown);

       
    }

    private void ResetShoot()
    {
        animator.SetBool("shoot", false);
        totalShoot--;
        readyToShoot = true;
        GameManager.instance.UpdateBall(totalShoot);

    }

    
    private int ShootNumber()
    { 

        minBallCount = PlayerPrefs.GetInt("houseHighscoreBall", minBallCount);
        obj = GameManager.instance.ObjectLifeLeft();
        if (minBallCount == 0) minBallCount = obj;
       
        totalShoot = Mathf.RoundToInt(Random.Range(minBallCount, (obj+(minBallCount/2))));
        
        GameManager.instance.SetBallBegin(totalShoot);
        
        return totalShoot;
    }
    
    
}
