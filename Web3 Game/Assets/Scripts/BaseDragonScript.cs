using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDragonScript : MonoBehaviour
{

    public Rigidbody2D baseDragonRigidBody;
    public float flyStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) == true)
        {
            baseDragonRigidBody.velocity = Vector2.up * flyStrength;
        }
    }
}
