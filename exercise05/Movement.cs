
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    public float speed;
    public float speedRotation;
    public GameObject prefab;
    public GameObject bulletPoint;
    public KeyCode SupportKey;
    public float jumpSpeed = 5;
    public float groundDistance = 0.5f;
    public KeyCode jumpKey; 
    Rigidbody rigidBody;
    bool doublejump;

    // Start is called before the first frame update

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //Jumping
        if (Input.GetKeyDown(jumpKey))
        {
            if(Physics.Raycast(transform.position, Vector3.down, groundDistance)){
                rigidBody.velocity = Vector3.up * jumpSpeed;
                doublejump = true;
            }
            //Activate DoubleJump
            else if(doublejump){
                rigidBody.velocity = Vector3.up * jumpSpeed;
                doublejump = false; //Avoids jumping twice
            }
        }
        
        //Foward Movement
        if(Input.GetKey(KeyCode.W)){
            //When running
            if(Input.GetKey(SupportKey))
                transform.Translate(0,0,speed*2f);
            //When Walking
            else
                transform.Translate(0,0,speed);
        }
        //Left Movement
        if(Input.GetKey(KeyCode.A)){
            //When running
            if(Input.GetKey(SupportKey))
                transform.Translate(-speed*2f,0,0);
            //When Walking
            else
                transform.Translate(-speed,0,0);
        }
        //Right Movement
        if(Input.GetKey(KeyCode.D)){
            //When running
            if(Input.GetKey(SupportKey))
                transform.Translate(speed*2f,0,0);
            //When Walking
            else
                transform.Translate(speed,0,0);
        }
        //Move Backwards
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0,0,-speed);
        }

        //Move arround cursor
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, speedRotation*mouseX, 0);

        //Shoot
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            GameObject clone = Instantiate(prefab);
            clone.transform.position = bulletPoint.transform.position;
            clone.transform.rotation = bulletPoint.transform.rotation;
        }
        
    }
    
}
