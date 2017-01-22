using UnityEngine;
using System.Collections;
using Characters;

public class CharacterLogic : MonoBehaviour
{
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f;
    public float inAirDamping = 5f;
    public float forcePush = 30f;
    public float jumpHeight = 3f;
    public float gravityModifier = 0.6f;
    public int maxJumps = 1;
    public float descendTime = 0.5f;
    public float attackTime = 0.25f;
    public float attackSpeed = 2f;
    public float attackDegradation = 100f;
    public float analogueDeadZone = 0.25f;
    bool jump;
    float gravityModificator = 1;
    float lastTimeDown = 0;
    int jumpsLeft = 0;
    float normalizedHorizontalSpeed = 0;
    public PlayerController playerController;
    CharacterController2D characterController;
    Vector3 velocity;
    SpriteRenderer spriteRenderer;
    public Vector2 aimingDirection;
    bool isPunching, hasBeenHit;
    WaitForSeconds playerYield;
    public PunchController arm;
    bool setUp;
    Vector2 facingDirection;
    public SpriteManager spriteManager;
    PowerStriker powerStriker;
    [Header("BottoWalls")]
    public LayerMask floorMask;
    public int maxWallCheckRays;
    public float raysBegin;
    public float wallCheckRayDistance;
    public float rayDistance;
    public AudioClip punchSound, jumpSound, dieSound;

    void Start()
    {
        characterController = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerYield = new WaitForSeconds(Time.deltaTime);
        characterController.onControllerCollidedEvent += onControllerCollider;
        characterController.onTriggerEnterEvent += onTriggerEnterEvent;
        characterController.onTriggerExitEvent += onTriggerExitEvent;
        aimingDirection.x = 1;
        arm.SetValues(transform);
        spriteManager = SpriteState.instance.GetRandoChibi();
        spriteManager.transform.SetParent(transform);
        powerStriker = spriteManager.GetComponent<PowerStriker>();
        spriteManager.transform.localPosition = Vector2.zero;
    }

    public void InitPlayer(InControl.InputDevice device)
    {
        playerController = PlayerController.CreateWithDefaultBindings(device);
        setUp = true;
    }

    void Update()
    {
        if (!setUp)
            return;
        // grab our current _velocity to use as a base for all calculations
        velocity = characterController.velocity;
        CastBottomWalls();
        if (hasBeenHit)
            return;
        if (characterController.isGrounded)
        {
            spriteManager.Land();
            velocity.y = 0;
            jumpsLeft = 0;
            jump = false;
        }

        if (playerController.Action3.IsPressed)
        {
            Punch();
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
                aimingDirection.x = -1;
        }
        if (playerController.Move.Right.IsPressed && Mathf.Abs(playerController.Move.X) > analogueDeadZone)
        {
            normalizedHorizontalSpeed = 1;
                aimingDirection.x = 1;
        }
        spriteManager.SetRunSpeed(normalizedHorizontalSpeed, runSpeed, characterController.isGrounded);

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
            spriteManager.PlayJump();
        }
        else {
            gravityModificator = 1;
        }
        GravityAndMovement(descend);
    }

    void GravityAndMovement(bool descend)
    {
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
            AudioManager.Instance.PlaySound(jumpSound);
        }
    }

    void Punch()
    {
        if (!isPunching && !hasBeenHit)
        {
            StartCoroutine(PunchLogic());
            AudioManager.Instance.PlaySound(punchSound);
        }
    }

    IEnumerator PunchLogic()
    {
        isPunching = true;
        arm.GetComponent<Collider2D>().enabled = true;
        float currentTime = 0f;
        Vector2 direction = aimingDirection.normalized;
        while (currentTime <= attackTime)
        {
            arm.transform.localPosition = (Vector2)arm.transform.localPosition + (direction * attackSpeed * Time.deltaTime);
            arm.SetCurrentValues(direction, forcePush);
            powerStriker.HitPosition(arm.transform.position);
            yield return playerYield;
            currentTime += Time.deltaTime;
        }
        arm.GetComponent<Collider2D>().enabled = false;
        while (arm.transform.localPosition != Vector3.zero)
        {
            arm.transform.localPosition = Vector2.MoveTowards(arm.transform.localPosition, Vector2.zero, attackSpeed * 0.5f* Time.deltaTime);
            powerStriker.HitPosition(arm.transform.position);
            yield return playerYield;
        }
        powerStriker.ResetPosition();
        isPunching = false;
    }

    public void Push(Vector2 Direction, float force)
    {
        if (hasBeenHit)
            return;
        hasBeenHit = true;
        spriteManager.ChangeColor(Color.red);
        StartCoroutine(PushBack(Direction, force));
    }

    IEnumerator PushBack(Vector2 direction, float force)
    {
        while (force > 0)
        {
            velocity = characterController.velocity;
            velocity = Vector2.MoveTowards(velocity, direction * force, Time.deltaTime * attackDegradation);
            CastBottomWalls();
            GravityAndMovement(false);
            force -= Time.deltaTime * attackDegradation;
            yield return playerYield;
        }
        spriteManager.ChangeColor(Color.white);
        hasBeenHit = false;
    }

    void CastBottomWalls()
    {
        var isGoingRight = normalizedHorizontalSpeed > 0;
        var rayDirection = Vector2.down;
        var rayPosition = new Vector2(transform.position.x, 0);
        float initialPos = transform.position.x + raysBegin;

        for (var i = 1; i < maxWallCheckRays; i++)
        {
            var ray = new Vector2(initialPos - i * wallCheckRayDistance, transform.position.y);
            var rayHit = Physics2D.Raycast(ray, rayDirection, rayDistance, floorMask);
            if (rayHit)
            {
                Vector2 objVelocity = rayHit.collider.GetComponent<Rigidbody2D>().velocity;
                if (objVelocity.y > 0.2f)
                {
                    velocity.y = objVelocity.y;
                    characterController.isGrounded = true;
                }
            }
            Debug.DrawRay(ray, rayDirection * rayDistance, Color.blue);
        }
    }

    #region Event Listeners

    public void onControllerCollider(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag("InstaDeath"))
        {
            AudioManager.Instance.PlaySound(dieSound);
            ParticlePooler.Instance.CreateFallingToLava(transform.position);
            Destroy(this.gameObject);
        } else if (hit.collider.CompareTag("VampiresHearth"))
        {
            Destroy(hit.transform.gameObject);
            Debug.Log("dolli won");
        }
    }

    public void onTriggerEnterEvent(Collider2D col)
    {
      
    }

    public void onTriggerExitEvent(Collider2D col)
    {

    }

    #endregion

    void OnBecameInvisible()
    {
        Debug.Log("Dolly died");
        AudioManager.Instance.PlaySound(dieSound);
        ParticlePooler.Instance.CreateOutOfScreen(transform.position);
        Destroy(this.gameObject);
    }
}
