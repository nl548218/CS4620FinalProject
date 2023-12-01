using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int playerHealth = 100;
    //public Canvas deathCanvas;

    public CharacterController controller; 

    public float speed = 12f; 
    public float gravity = -9.81f; 

    public Transform groundCheck;

    public float jumpHeight = 3f; 

    //radius of sphere used to check for ground 
    public float groundDistance = 0.4f;

    //Used for what objects this sphere will check
    public LayerMask groundMask; 
    public Animator anim; 
    Vector3 velocity; 
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
       // deathCanvas.enabled = false;
        anim = GetComponentInChildren<Animator>(); 

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f; 
        }

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");
       

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        }
        
      /*  if(Input.GetMouseButtonDown(0)){
            anim.SetBool("swing", true);
        }else{
            anim.SetBool("swing", false);
        }*/

        
        anim.SetFloat("forward", z);
        //anim.SetFloat("strafe", x);

        

        Vector3 move  = transform.right * x + transform.forward * z; 

        controller.Move(move * speed * Time.deltaTime); 

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        playerHealth -= 25;
        if(playerHealth <= 0)
        {
            deathCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }*/
}
