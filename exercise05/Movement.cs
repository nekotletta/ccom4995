
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    public float speed;
    public float speedRotation;
    public float jumpSpeed;
    public GameObject prefab;
    public GameObject bulletPoint;
    public KeyCode actionKey;
    public KeyCode SupportKey;
    public float actionTime = 5f;
    private bool isActionActive = false;
    private float timer = 0f;

    // Start is called before the first frame update

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        //activar botón para brincar
        if (Input.GetKeyDown(actionKey))
        {
            // Iniciar la acción
            isActionActive = true;
            timer = 0f;
        }
        if (isActionActive)
        {
            // Realizar la acción durante el tiempo especificado
            timer += Time.deltaTime;
            if (timer >= actionTime/5)
            {
                // Finalizar la acción
                isActionActive = false;
                timer = 0f;
                Debug.Log("Acción completada");
            }
            else
            {
                // Realizar la acción mientras el tiempo no se haya cumplido
                // Aquí puedes colocar el código de la acción que deseas ejecutar
                // durante el tiempo especificado
                transform.Translate(0,jumpSpeed,0);
                Debug.Log("Realizando acción...");
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
