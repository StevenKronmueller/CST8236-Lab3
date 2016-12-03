using UnityEngine;
using System.Collections;
public class MovingSoccerBall : MonoBehaviour
{
    public float radius = 9.0f;
    public float force = 999.0f;
    // for initialization
    void Start()
    {
    }

    // called once per frame
    void Update()
    {
         Vector3 ballDirection = new Vector3();

         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
             ballDirection.z = 1;
         }
         else if (Input.GetKeyDown(KeyCode.DownArrow))
         {
             ballDirection.z = -1;
         }
         else if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
             ballDirection.x = -1;
         }
         else if (Input.GetKeyDown(KeyCode.RightArrow))
         {
             ballDirection.x = 1;
         }
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 0.0f, (Input.GetAxis("Vertical") * Time.deltaTime));
        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, ballDirection, out hit, ballDirection.magnitude);

        if (didHit)
        {
            Vector3 explosion = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosion, radius);
            foreach (Collider collider in colliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.isKinematic = false;
                    //used to simulate explosion effects
                    rigidbody.AddExplosionForce(force, explosion, radius, 3.0f);
                }
            }
        }
        else {
            transform.position += ballDirection;
        }
    }
}