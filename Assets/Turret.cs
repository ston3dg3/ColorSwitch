using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;
    private bool targetLocked;
    private bool shotReady;

    public bool changer;
    public float accuracy = 5f;
    public float fireTimer;
    public GameObject bullet;
    public GameObject colorChanger;
    public GameObject bulletSpawnPoint;
    public int fireMode;                // 0 is semi, 1 is burst, 2 is auto (auto not implemented cuz its stoopid)
    public int numOfShots = 3;              // specify number of shots in a single burst shot

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 1f;
        targetLocked = false;
        shotReady = true;
    }

    void Shoot()
    {
        switch (fireMode)
        {
            case 0:         // semi fire mode
                spawnBullet();
                shotReady = false;
                StartCoroutine(FireRate());
                return;

            case 1:         // burst fire mode
                float time = 0f;
                for(int i = 0; i < numOfShots; i++)
                {
                    Invoke("spawnBullet", time);
                    time += 0.08f;
                }
                shotReady = false;
                StartCoroutine(FireRate());
                return;

            default:
                spawnBullet();
                shotReady = false;
                StartCoroutine(FireRate());
                return;
        }
    }

    void spawnBullet()
    {
        if(!changer)
        {
            Transform _bullet = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
            // _bullet.transform.rotation = bulletSpawnPoint.transform.rotation;


            Vector3 aimAngle = bulletSpawnPoint.transform.eulerAngles;
            // generate random float from -4 to +4 angles
            float celnoscSzturmowca = Random.Range(-accuracy,accuracy);
            // insert it into a vector3 so we can add it to the original aimAngle
            Vector3 katRazenia = new Vector3(0f,0f,celnoscSzturmowca);
            // add the 2 angles
            Vector3 ogienOstateczny = aimAngle + katRazenia;
            // substitute the original angle of fire with ogienOstateczny
            _bullet.transform.eulerAngles = ogienOstateczny;

        }
        else
        {
            Transform _bullet = Instantiate(colorChanger.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
            _bullet.transform.rotation = bulletSpawnPoint.transform.rotation;
            _bullet.gameObject.AddComponent<BulletMovement>();
            Destroy(_bullet.gameObject.GetComponent<rotator>());
        }
        
    }

    void Update()
    {
        if (targetLocked)
        {
            transform.LookAt(target.transform);
            transform.Rotate(0,-90,-90);

            if(shotReady)
            {
                Shoot();
            } else
            {
                dontShoot();
            }
        }
    }

    void dontShoot()
    {
        // CEASE FIREE !! CEASE FIRE!!!
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            targetLocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = null;
            targetLocked = false;
            StopCoroutine(FireRate());
        }
    }
}
