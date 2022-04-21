using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class IconController : MonoBehaviour
{
    
    public MainController MainController;
  
    public bool isActive = false;
    public float enlargedScale = 2;
    public float enlargedHeith = 4;
    public float enlargeSpeed = 4;
    public float initialX;
    public float initialY;
    private UnityAction someListener;
    private bool lastActive;

    void Awake()
    {
        someListener = new UnityAction(Reset);
    }
    void OnEnable()
    {
        EventManager.StartListening("Reset", someListener);
    }
    void OnDisable()
    {
        EventManager.StopListening("Reset", someListener);
    }
    // Start is called before the first frame update
    void Start()
    {
        enlargedScale = MainController.enlargeScale;
        enlargedHeith = MainController.enlargeHeight;
        enlargeSpeed = MainController.enlargeSpeed;
        initialX = transform.position.x;
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (transform.localScale.x < enlargedScale)
            {
                transform.localScale += new Vector3(Time.deltaTime * enlargeSpeed, Time.deltaTime * enlargeSpeed, Time.deltaTime * enlargeSpeed);
            }
            else
            {
                transform.localScale = new Vector3(enlargedScale, enlargedScale, enlargedScale);
            }

            if (transform.position.y < enlargedHeith)
            {
                transform.position += new Vector3(0, Time.deltaTime * enlargeSpeed, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, enlargedHeith, transform.position.z);
            }
        }
        else
        {
            if (transform.localScale.x >= 1)
            {
                transform.localScale -= new Vector3(Time.deltaTime * enlargeSpeed, Time.deltaTime * enlargeSpeed, Time.deltaTime * enlargeSpeed);
                if (transform.localScale.x < 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (transform.position.y >= initialY)
            {
                transform.position -= new Vector3(0, Time.deltaTime * enlargeSpeed, 0);
                if (transform.position.y < initialY)
                {
                    transform.position += new Vector3(0, initialY - transform.position.y, 0);
                }
            }
        }
    }
    void OnMouseUp()
    {
        Debug.Log(gameObject.name + " get click");
        MainController.SendCommand(gameObject.name);


        if (lastActive)
        {
            EventManager.TriggerEvent("Reset");            
        }
        else
        {
            EventManager.TriggerEvent("Reset");
            isActive = true;// !isActive;
        }
    }

    public void Reset()
    {
        lastActive = isActive;
        //Debug.Log(gameObject.name + " reseved message");
        isActive = false;
    }
}