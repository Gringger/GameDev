using System.Collections;

using System.Collections.Generic;


using UnityEngine;





public class HeroRabit : MonoBehaviour
{
    Rigidbody2D myBody = null;
    Animator _animatorController;
    Transform position;
    public float speed = 1;
    // Use this for initialization
    void Start()
    {
       position = GetComponent<Transform>();
         myBody = this.GetComponent<Rigidbody2D>();
        _animatorController = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float value1 = Input.GetAxis("Vertical");
        if (Mathf.Abs(value1) > 0 )
        {
            _animatorController.Play("Jump");
        }
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0 && position.localPosition.y < 0)
        {
            _animatorController.Play("WalkRight");
        
        }
        else if (position.localPosition.y<0){
                _animatorController.Play("Idle");
            }
    }
    void FixedUpdate()
    {

        float value1 = Input.GetAxis("Vertical");
        if (Mathf.Abs(value1) > 0 )
        {
            Vector2 vel = myBody.velocity;
            vel.y = value1 * speed;
            myBody.velocity = vel;
        }
        //[-1, 1]
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            _animatorController.Play("WalkRight");
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

}
