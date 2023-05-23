using UnityEngine;

public class P1 : MonoBehaviour
{
    public CharacterController cc;
    public Transform cam;
    //public NavMeshAgent agent;
    private Animator anim;
    private bool isJump;
    [Header("Speed:")]
    public float runMultiplier;
    public float gravity = -9.81f;
    Vector3 velocity;
    public float speed = 6f;
    public float jumpSpeed = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private float h;
    private float v;
    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        cam = Camera.main.transform;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    //状态信息
    AnimatorStateInfo stateinfo;

    private void Update()
    {
        v = Input.GetAxis("Horizontal");
        h = Input.GetAxis("Vertical");

        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 direction = new Vector3(v, 0, h).normalized;

        Vector3 moveDir;
        if (direction.magnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Debug.Log(targetAngle);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            cc.Move(moveDir.normalized * speed * Time.deltaTime);
            anim.SetFloat("Speed", 1);
            stateinfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateinfo.IsName("Jump_AR"))
            {
                cc.Move(moveDir.normalized * speed * Time.deltaTime * 2);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                cc.Move(moveDir.normalized * Time.deltaTime * runMultiplier);
            }
        }
        else
        {
            moveDir = Vector3.zero;
            anim.SetFloat("Speed", 0);
        }
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -0.8f * gravity);
            anim.SetTrigger("Jump");
        }
    }
}


