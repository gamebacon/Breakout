using System;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
    private int health = 4;
    private int points = 100;
    private Vector3 rotator = new Vector3(45, 0, 0);
    
    public Material hitMaterial;
    
    [SerializeField] private Material[] mats;

    
    private Renderer _renderer;
    
    
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        
        transform.Rotate(rotator * (transform.position.x + transform.position.y) * .03f);
        
        health = Random.Range(1,4);
        points = health * 100;
        Debug.Log(health);
        UpdateMat();
    }

    private void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        AudioManager.Play("hit", 1);//Random.Range(.8f, 1.2f));
        
            GameManager.UpdateScore(10);
        
        if (--health <= 0)
        {
            GameManager.UpdateScore(this.points);
            Destroy(gameObject);
        }
        else
        {
            VisualHit();
        }
        
        
    }

    private void VisualHit()
    {
        _renderer.sharedMaterial = hitMaterial;
        Invoke("UpdateMat", .05f);
    }

    private void UpdateMat()
    {
        /* 4 - 3 purp
         * 3 - 2 red
         * 2 - 1 blue
         * 1 - 0 yellow
         */

        try
        {
            _renderer.sharedMaterial = mats[health - 1];
        }
        catch (Exception e)
        {
            Debug.Log("hp: " + (health-1));
            Debug.Log("mats: " + mats.Length);
            
        }
        
    }

}
