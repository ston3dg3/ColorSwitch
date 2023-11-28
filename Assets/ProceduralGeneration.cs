using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{

    // general initialization
    [SerializeField] float TotalHeigth;
    [SerializeField] int loopRun;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject colorChanger;
    [SerializeField] GameObject rotator;
    [SerializeField] GameObject stripe;
    [SerializeField] GameObject quarter;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject hostileTurret;
    
    [SerializeField] GameObject player;
    [SerializeField] Player playerScript;
    [SerializeField] bool playerAlive;

    public SpawnerClass[] objects; // for holding all probability spawned objects
    mover[] MoverScripts;
    
    // Distance initialization
    [SerializeField] int minSpace;
    [SerializeField] int maxSpace;
    [SerializeField] float referenceRadius;
    [SerializeField] float radius1;
    [SerializeField] float radius2;
    [SerializeField] float size;
    [SerializeField] float distance;

    // Rotating things initialization
    [SerializeField] int maxSpeed;
    [SerializeField] int minSpeed;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [SerializeField] int circleCount;

    // Stripe initialization
    [SerializeField] int minSpeedStripe;
    [SerializeField] int maxSpeedStripe;

    public int hundred;
    public bool skip;
    public int x1;
    public int x2;
    

    void Awake()
    {
        hundred = 1;
        skip = false;
        x1 = 0;
        x2 = 200;
        playerAlive = true;
    }

    public void die()
    {
        playerAlive = false;
    }

    void Start()
    {
        referenceRadius = 2.47f;
        radius1 = referenceRadius;
        minSpace = 0;
        maxSpace = 4;
        minSpeed = 100;
        maxSpeed = 200;
        minSize = 1.5f;
        maxSize = 3.0f;
        distance = 0f;

        TotalHeigth = 200f;
        minSpeedStripe = 3;
        maxSpeedStripe = 7;
        Generation();
        // StartCoroutine(Generate());
    }

    void Update()
    {
        while(playerAlive);
        {
            // if (player.transform.position.y > 100f * (float)hundred)
            // {
            //     if(hundred % 2 == 0) 
            //     {
            //         skip = true;
            //     }
            //     else 
            //     {
            //         skip = false;
            //     }

            //     hundred++;

            //     if (!skip)
            //     {
            //         x1 = x2;
            //         x2 += 200;
            //         Generation(x1, x2);
            //     }
            // }          
        }
        
    }

    // IEnumerator Generate()
    // {
    //     if(playerAlive)
    //     {

    //     }
    //     yield return Generation();
    //     shotReady = true;
    // }

    void Generation(int lowest = 0, int maximum = 200)
    {
        float x = 5.0f + (float)lowest;
        circleCount = 0;
        loopRun = 0;
        float maxx = (float)maximum;
        while(x < maxx)
        {
            // SPAWN a circle not overlapping other circles on y axis
            float space = (float)Random.Range(minSpace, maxSpace+1);
            float newSize = Random.Range(minSize, maxSize);
            radius2 = newSize * referenceRadius;
            distance = radius1 + radius2;
            if(loopRun == 0) distance += space;
            if(distance < 10f) distance += space;
            radius1 = radius2;

            int i = Random.Range(0, 100);

            for(int j = 0; j < objects.Length; j++)
            {
                if(i >= objects[j].minProbabilityRange && i <= objects[j].maxProbabilityRange)
                {
                    int tempSpeed;
                    void spawnTurret()
                    {
                        if(x>15)
                        {
                            if(randomize(4))
                            {
                                turret = hostileTurret;
                            } 
                            float high = x;
                            high = x-(distance/2);
                            if(randomize())
                            {
                                spawnObj(turret, high, 0f, -6.6f, 0, false);
                            } else
                            {
                                spawnObj(turret, high, 0f, 6.6f, 0, true);
                            }
                        }
                        
                    }

                    switch (objects[j].spawnObject.tag)
                    {
                        case "Circle":
                            tempSpeed = generateRandomSpeed(90, 130);
                            spawnObj(circle, x, newSize, 0f, tempSpeed);
                            if (newSize <= 1.7f)
                            {
                                spawnObj(circle, x, newSize+1, 0f, tempSpeed);
                            }
                            if(randomize(4)) spawnTurret();
                            break;

                        case "Rotator":
                            tempSpeed = generateRandomSpeed(50, 120);
                            spawnObj(rotator, x, newSize, -2.5f, tempSpeed, false);
                            spawnObj(rotator, x, newSize, 2.5f, -tempSpeed, true);
                            if(randomize(3)) spawnTurret();
                            break;

                        case "Stripe":
                            spawnObj(stripe, x);
                            if(randomize(2)) spawnTurret();
                            break;

                        case "Quarter":
                            spawnObj(quarter, x, newSize);
                            if(randomize(3)) spawnTurret();
                            break;

                        case "Turret":
                            
                            break;

                        default:
                            break;
                    }
                    break;
                }
            }
            
            // SPAWN a ColorChanger in a circle (center) or midpoint between 2 circles!
            int countToNextChanger = Random.Range(2,3);  // controls the frequency of ColorChangers
            circleCount++;
            if(circleCount == countToNextChanger)
            {
                circleCount = 0;
                float high = x;
                high = x-(distance/2);
                if (loopRun != 0) spawnObj(colorChanger, high);
            }
            
            x = x + distance;
            loopRun++;
            
        }
    }

    void spawnObj(GameObject obj, float heigth, float size = 0f, float offset = 2.5f, int rotation = 100, bool rotate = false)
    {
        if (obj.tag == "Circle")
        {
            obj = Instantiate(obj, new Vector2(0f, heigth), Quaternion.identity);
            setRandomScale(obj, size);
            if (rotation != 100)
            {
                obj.GetComponent<rotator>().setSpeed(rotation);
            } else {
                setRandomSpeed(obj);
            }
        } 

        if (obj.tag == "Rotator")
        {
            if(rotate)
            {
                obj = Instantiate(obj, new Vector2(offset, heigth), Quaternion.Euler(new Vector3(0,0,45)));
            }
            else
            {
                obj = Instantiate(obj, new Vector2(offset, heigth), Quaternion.identity);
            }
            obj.GetComponent<rotator>().setSpeed(rotation);
        }

        if (obj.tag == "ColorChanger")
        {
            obj = Instantiate(obj, new Vector2(0f, heigth), Quaternion.identity);
            // set custom color to change to
            // obj.GetComponent<rotator>().setSpeed(speed);
        } 

        if (obj.tag == "Stripe")
        {
            obj = Instantiate(obj, new Vector2(0f, heigth), Quaternion.identity);
            setRandomSpeed(obj);
        } 

        if (obj.tag == "Turret")
        {
            if(rotate)
            {
                obj = Instantiate(obj, new Vector2(offset, heigth), Quaternion.Euler(new Vector3(0,180,0)));
            }
            else
            {
                obj = Instantiate(obj, new Vector2(offset, heigth), Quaternion.identity);
            }
        } 

        if (obj.tag == "Quarter")
        {
            obj = Instantiate(obj, new Vector2(0f, heigth), Quaternion.Euler (0f, 0f, 45f)); // set the rotation to always be 45 degrees at spawn
            setRandomSpeed(obj);
            setRandomScale(obj, size);
        } 

        obj.transform.parent = this.transform;
    }

    public bool randomize(float probab = 2)
    {       
            float randomFloat = Random.Range(0f,2f);
            Debug.LogWarning("Random float: "+randomFloat);
            if(randomFloat >= 1.5f)
            { 
                return true;
            }
            return false;
    }

    void setRandomSpeed(GameObject obj)
    {
        if(obj.tag == "Circle" || obj.tag == "ColorChanger" || obj.tag == "Rotator")
        {
            int speed = Random.Range(minSpeed, maxSpeed+1);
            obj.GetComponent<rotator>().setSpeed(speed);
        }
        if(obj.tag == "Stripe")
        {
            int speed = Random.Range(minSpeedStripe, maxSpeedStripe+1);
            if(randomize()) speed = -speed;
            MoverScripts = obj.GetComponentsInChildren<mover>();
            MoverScripts[0].setSpeed(speed);
        }
        if(obj.tag == "Quarter")
        {
            float freq = Random.Range(0.3f, 0.7f);
            obj.GetComponent<quarterRotator>().setFrequency(freq);
        }
        
    }

    private int generateRandomSpeed(int minSped = 0, int maxSped = 0)
    {
        if (minSped == 0 && maxSped == 0)
        {
            minSped = minSpeed;
            maxSped = maxSpeed;
        }
        int speed = Random.Range(minSped, maxSped+1);
        return speed;
    }
    
    void setRandomScale(GameObject obj, float size)
    {
        Vector3 newScale = new Vector3(size, size, 1f);
        obj.transform.localScale = newScale;
    }

}

[System.Serializable]
public class SpawnerClass
{
    public GameObject spawnObject;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;

}
