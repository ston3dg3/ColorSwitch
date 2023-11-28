using UnityEngine;

public class mover : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float length = 13.35f;
    public Transform[] Stripes;
    private GameObject stripeObj1;
    private GameObject stripeObj2;
    private Transform Stripe1;
    private Transform Stripe2;
    private Transform parentTransform;

    void Start()
    {
        Stripes = new Transform[2];
        stripeObj1 = this.gameObject;
        Stripe1 = stripeObj1.GetComponent<Transform>();
        parentTransform = Stripe1.parent.transform; 
        stripeObj2 = parentTransform.Find("Stripe2").gameObject;
        Stripe2 = stripeObj2.GetComponent<Transform>();
        Stripes[0] = Stripe1;
        Stripes[1] = Stripe2;
    }

    void Update()
    {
        Stripe1.Translate(speed * Time.deltaTime, 0f, 0f);
        Stripe2.Translate(speed * Time.deltaTime, 0f, 0f);
        //transform.Translate(speed * Time.deltaTime, 0f, 0f);
        
        if (speed < 0)
        {
            foreach (Transform transf in Stripes)
            {
                Vector3 newStripePos = transf.position;
                if (newStripePos.x < -length)
                {
                    newStripePos.x += 2 * length;
                }
                transf.position = newStripePos;
            }
        }
        if (speed > 0)
        {
            foreach (Transform transf in Stripes)
            {
                Vector3 newStripePos = transf.position;
                if (newStripePos.x > length)
                {
                    newStripePos.x -= 2 * length;
                }
                transf.position = newStripePos;
            }
        }
        
    }
    public void setSpeed(int sped)
    {
        speed = sped;
    } 
}
