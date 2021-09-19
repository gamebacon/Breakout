using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody _rg;
    private Vector3 _velocity;
    private float speed = 30;
    
    void Start()
    {
        _rg = GetComponent<Rigidbody>();
        Invoke("launch", .5f);
    }

    private void launch()
    {
        _rg.velocity = Vector3.down * speed;
    }

    private void FixedUpdate()
    {
        _rg.velocity = _rg.velocity.normalized * speed;
        _velocity = _rg.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 normal = other.contacts[0].normal;
            
        
        if (other.gameObject.tag.Equals("wall"))
        {
            AudioManager.Play("hit_wall");
            //prevent ball getting stuck on one axis
            normal += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f),0);
            //Debug.Log(normal.ToString());
        }

        _rg.velocity = Vector3.Reflect(_velocity, normal);
    }
}
