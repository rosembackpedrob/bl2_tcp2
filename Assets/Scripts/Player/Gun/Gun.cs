using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    [SerializeField] private TextMeshProUGUI ammoText;
    public float reloadTime = 1f;
    private bool isReloading = false;
    
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private ParticleSystem muzzleFlash;
    
    private float nextFire = 0f;

    public Animator animator;

    Transform cam;

    
    void Awake()
    {
        cam = Camera.main.transform;
    }
    
    void Start() 
    {
        
        currentAmmo = maxAmmo;    
    }
    void OnEnable() 
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void Shoot()
    {
        //reloading update
        if(isReloading)
            return;
        
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        FindObjectOfType<AudioManager>().Play("Pistol");
        currentAmmo--;
        
        if(Time.time >= nextFire)
        {
            nextFire = Time.time + 1f/fireRate;
            RaycastHit hitInfo;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
            {
                Debug.Log(hitInfo.transform.name);

                muzzleFlash.Play();

                Target target = hitInfo.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hitInfo.rigidbody != null)
                {
                    hitInfo.rigidbody.AddForce(hitInfo.normal * (- impactForce));
                }

                GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO, 2f);
            }
        }

        ammoText.text = "ammo: " + currentAmmo;
    }
}
