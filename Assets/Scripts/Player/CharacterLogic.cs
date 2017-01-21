using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour
{
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f;
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;
    public float gravityModifier = 0.6f;
    public int maxJumps = 1;
    public float descendTime = 0.5f;
    public float analogueDeadZone = 0.25f;
    bool jump;
    float gravityModificator = 1;
    float lastTimeDown = 0;
    int jumpsLeft = 0;
    float normalizedHorizontalSpeed = 0;
    PlayerController playerController;
    CharacterController2D characterController;
    Vector3 velocity;
    SpriteRenderer spriteRenderer;
    public Vector2 aimingDirection;
    void Start()
    {
        playerController = PlayerController.CreateWithDefaultBindings();
        characterController = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterController.onControllerCollidedEvent += onControllerCollider;
        characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        characterController.onTriggerExitEvent += onTriggerExitEvent;
    }

    void Update()
    {
        // grab our current _velocity to use as a base for all calculations
        velocity = characterController.velocity;

        if (characterController.isGrounded)
        {
            velocity.y = 0;
            jumpsLeft = 0;
            jump = false;
        }

        if (playerController.Action3.IsPressed)
        {
            
        }

        if (playerController.Action1.WasPressed)
        {
            PerformJump();
        }

        bool descend = false;
        if (playerController.Move.Down.WasReleased)
        {
            lastTimeDown = Time.time;
        }

        normalizedHorizontalSpeed = 0;
        
        playerController.Move.Raw = true;

        if (playerController.Move.Down.IsPressed && Mathf.Abs(playerController.Move.Y) > analogueDeadZone)
        {
            if (Time.time - lastTimeDown < descendTime)
            {
                descend = true;
            }
        }
        aimingDirection.x = -transform.localScale.x;
        // Control vertical aim of weapon
        if ((playerController.Move.Up.IsPressed || playerController.Move.Down.IsPressed) && Mathf.Abs(playerController.Move.Y) > analogueDeadZone)
        {
            float moveAngle = (Mathf.Abs(playerController.Move.X) > analogueDeadZone && characterController.isGrounded)  ? 45 : 90;

            if (playerController.Move.Y > 0)
            {
                aimingDirection.y = Mathf.Lerp(aimingDirection.y, 1, 2f * Time.deltaTime);
                aimingDirection.x = 0;
            }
            else if (playerController.Move.Y < 0)
            {
                aimingDirection.y = Mathf.Lerp(aimingDirection.y, -1, 2f * Time.deltaTime);
                aimingDirection.x = 0;
            }
        }
        else
        {
           aimingDirection.y = Mathf.Lerp(aimingDirection.y, 0, 2f* Time.deltaTime);
        }

        if (playerController.Move.Left.IsPressed && Mathf.Abs(playerController.Move.X) > analogueDeadZone)
        {
            normalizedHorizontalSpeed = -1;
            transform.localScale = new Vector3(1, 1, 1);
            if(aimingDirection.y != 0 && characterController.isGrounded)
                aimingDirection.x = -1;
        }
        if (playerController.Move.Right.IsPressed && Mathf.Abs(playerController.Move.X) > analogueDeadZone)
        {
            normalizedHorizontalSpeed = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            if (aimingDirection.y != 0 && characterController.isGrounded)
                aimingDirection.x = 1;
        }


        if (playerController.RightBumper.WasReleased)
        {

        }

        if (playerController.LeftBumper.WasReleased)
        {

        }

        // apply horizontal speed smoothing it
        var smoothedMovementFactor = characterController.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        velocity.x = Mathf.Lerp(velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        //we are falling, modify gravity for better control?
        if (playerController.Action1.IsPressed && velocity.y > 0 && jump)
        {
            gravityModificator = gravityModifier;
        }
        else {
            gravityModificator = 1;
        }

        velocity.y += (gravity * gravityModificator) * Time.deltaTime;


        characterController.move(velocity * Time.deltaTime, descend);
    }

    void PerformJump()
    {
        if (characterController.isGrounded || (jumpsLeft < maxJumps))
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            jumpsLeft++;
            jump = true;
        }
    }

    #region Event Listeners

    public void onControllerCollider(RaycastHit2D hit)
    {
    }


    public void onTriggerEnterEvent(Collider2D col)
    {
      
    }

    public void onTriggerExitEvent(Collider2D col)
    {

    }

    #endregion
}
