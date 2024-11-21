using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] GameObject playerVisual;

    Vector3 targetPosition;
    float horizontal;
    float vertical;
    private Joystick joystick;
    public Joystick Joystick { get; set; }
    private void FixedUpdate()
    {
        if (targetEnemy != Vector3.zero && !GetInPut())
        {
            Attack();
        }
        currentTime -= Time.deltaTime;
        FindEnemy(transform.position, radius);

    }
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay) && GetInPut())
        {

            MovePlayer();
        }
        else
        {

            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }

    }

    public void OnInit()
    {
        int currenIndexWeapon = PlayerPrefs.GetInt("currrenWeapon");
        ChangeWeapon((WeaponType)currenIndexWeapon);



    }
    public void SetJoystick()
    {
        horizontal = Joystick.Horizontal;
        vertical = Joystick.Vertical;

    }
    private bool GetInPut()
    {
        if (Joystick != null)
        {
            SetJoystick();
            if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f)
            {
                return false;
            }
            return true;
        }
        return false;


    }
    private void MovePlayer()
    {
        Vector3 direction = Vector3.forward * Joystick.Vertical + Vector3.right * Joystick.Horizontal;
        if (direction != Vector3.zero)
        {
            ChangeAnim(Constants.RUN_ANIM_NAME);
            targetPosition = transform.position + direction * speedMove * Time.deltaTime;
            Vector3 lookDirection = direction + playerVisual.transform.position;
            playerVisual.transform.LookAt(lookDirection);
        }
        else
        {
            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.ChangeState(GameState.Pasue);
        UIManager.Instance.OpenUI<CanvasFail>();
    }
}
