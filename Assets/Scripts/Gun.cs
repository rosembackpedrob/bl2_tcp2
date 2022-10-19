using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    Transform cam;

    // Update is called once per frame
    void Awake()
    {
        cam = Camera.main.transform;
    }

    public void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
        }

    }
}
