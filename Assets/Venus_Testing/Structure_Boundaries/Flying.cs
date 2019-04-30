using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour
{

    //initial speed
    public int speed = 20;

    // Turning script
    Vector2 rotation = new Vector2(0, 0);
    public float Tspeed = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //For the following 'if statements' don't include 'else if', so that the user can press multiple buttons at the same time
        //move camera to the left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + Camera.main.transform.right * -1 * speed * Time.deltaTime;
        }

        //move camera backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + Camera.main.transform.forward * -1 * speed * Time.deltaTime;

        }
        //move camera to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Camera.main.transform.right * speed * Time.deltaTime;

        }
        //move camera forward
        if (Input.GetKey(KeyCode.W))
        {

            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }
        //move camera upwards
        if (Input.GetKey(KeyCode.E))
        {
            transform.position = transform.position + Camera.main.transform.up * speed * Time.deltaTime;
        }
        //move camera downwards
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = transform.position + Camera.main.transform.up * -1 * speed * Time.deltaTime;
        }

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * Tspeed;
    }
}