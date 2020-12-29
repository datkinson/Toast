using System;
using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MovementForce = 20;
        [SerializeField] public float m_JumpForce = 250f;                  // Amount of force added when the player jumps.
        [SerializeField] private Vector2 m_WallJumpForce = new Vector2(300f, 100f);
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        [SerializeField] public Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        [SerializeField] public Transform m_WallCheck;    // A position marking where to check if the player is touching a wall.
        [SerializeField] public Transform m_WallCheckBack;    // A position marking where to check if the player is touching a wall from behind.
        const float k_WalledRadius = .1f; // Radius of the overlap circle to determine if walled
        private bool m_Walled;            // Whether or not the player is walled.
        [SerializeField] public Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private int jumpsLeft = 1;
        private int maxJumps = 1;
        private bool canWallJump = true;

        private void Awake()
        {
            // Setting up references.
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            m_Walled = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject) {
                    m_Grounded = true;
                    jumpsLeft = maxJumps;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);


            Collider2D[] wallColliders = Physics2D.OverlapCircleAll(m_WallCheck.position, k_WalledRadius, m_WhatIsGround);
            for (int i = 0; i < wallColliders.Length; i++)
            {
                if (wallColliders[i].gameObject != gameObject) {
                    m_Walled = true;
                    // Debug.Log("Walled: True");
                }
            }
            Collider2D[] ceilingColliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
            for (int i = 0; i < ceilingColliders.Length; i++)
            {
                if (ceilingColliders[i].gameObject != gameObject) {
                    Debug.Log("Y Velocity: " +  m_Rigidbody2D.velocity.y);
                    m_Rigidbody2D.AddForce(new Vector2(0, -m_Rigidbody2D.velocity.y * m_Rigidbody2D.mass));
                }
            }
            if (!m_Walled) {
                canWallJump = true;
            }

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool jump)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            // m_Rigidbody2D.velocity = new Vector2(move*m_MovementForce, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.AddForce(new Vector2(move*m_MovementForce, 0));

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                Debug.Log("Jump");
                // jumpsLeft -= 1;
            }else {
                if (m_Walled && jump && canWallJump)
                {
                    // Add a vertical force to the player.
                    if (m_FacingRight) {
                       m_Rigidbody2D.AddForce(m_WallJumpForce * new Vector2(-1, 1));
                    } else {
                       m_Rigidbody2D.AddForce(m_WallJumpForce);
                    }
                    // Add a horizontal force to the player.
                    // m_Anim.SetFloat("Speed", Mathf.Abs(m_MovementForce));
                    // m_Rigidbody2D.velocity = new Vector2(-m_MovementForce, -m_Rigidbody2D.velocity.y);
                    // Debug.Log("Wall Jump");
                    canWallJump = false;
                    Flip();
                }
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
