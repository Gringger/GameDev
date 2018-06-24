using System.Collections;

using System.Collections.Generic;


using UnityEngine;





public class HeroRabit : MonoBehaviour
{
    public float timer = 10.0f;
    public bool isBig = false;
    Rigidbody2D myBody = null;
    public Animator animator;
    public float speed = 1;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    Transform heroParent;
    float currentDieTime = 0;
     float dieTime = 1f;
    
    // Use this for initialization
    void Start()
    {
        heroParent = transform.parent;
        float diff = Time.deltaTime;
      
         myBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        LevelController.current.setStartPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
           

        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            
            animator.SetBool("run", true);
        }
        else
        {   
            animator.SetBool("run", false);
        }
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        { 
            animator.SetBool("jump", true);

        }
        if (isBig == true) { 
            timer -= 1.0f * Time.deltaTime;
            if (timer <= 0.0f)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                timer = 10.0f;
                isBig = false;
            }
        }

        
            


        }
        
        
    
    void FixedUpdate()
    {

        //[-1, 1]
        if (animator.GetBool("dead") == false)
        {
            float value = Input.GetAxis("Horizontal");
            if (Mathf.Abs(value) > 0)
            {
                Vector2 vel = myBody.velocity;
                vel.x = value * speed;
                myBody.velocity = vel;
            }
       
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
 }
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(transform, hit.transform);
            }
        }
        else
        {
            isGrounded = false;
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(transform, heroParent);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
        
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        if (animator.GetBool("dead"))
        {

            currentDieTime -= Time.deltaTime;
            if (currentDieTime <= 0)
            {
                LevelController.current.onRabitDeath(this);
                animator.SetBool("dead", false);
                currentDieTime = dieTime;
            }
        }
    
        
            
        
    }
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
   
    
    public void Die()
    {

        animator.SetBool("dead", true);
        currentDieTime = dieTime;


    }

 

}
