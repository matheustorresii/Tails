using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;
    public BoxCollider2D boxCol;
    public CircleCollider2D circleCol;

    public Text scoreText;

    public float movementSpeed;

    int score;

    float horizontalMovementSpeed;
    bool isJumping = false;
    bool isHurt = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        isJumping = false;
        isHurt = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;

        anim.SetFloat("speed", Mathf.Abs(horizontalMovementSpeed));

        if(Input.GetButtonDown("Jump")) {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMovementSpeed * Time.fixedDeltaTime, false, isJumping);
    }

    public void OnLanding() {
        isJumping = false;
        anim.SetBool("isJumping", false);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Enemy") {
            if(isJumping) {
                Destroy(col.gameObject);
                setScore();
            } else {
                anim.SetBool("isHurt", true);
                boxCol.enabled = false;
                circleCol.enabled = false;
            }
        }
        if(col.gameObject.tag == "Gem") {
            setScore();
            Destroy(col.gameObject);
        }
    }

    private void setScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
