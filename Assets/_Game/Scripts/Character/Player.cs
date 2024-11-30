using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] GameObject playerVisual;
    [SerializeField] LookEnemy lookEnemyPrefab;
    private Transform botLookEnemy;
    Vector3 positionMove;
    float horizontal;
    float vertical;
    private Joystick joystick;
    public Joystick Joystick { get =>joystick; set => joystick =value; }


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
        if (targetEnemy != Vector3.zero && !GetInPut())
        {
            Attack();
        }
        currentTime -= Time.deltaTime;
        FindEnemy(transform.position, radius);
        LookTargetEnemy();
    }

    public void OnInit()
    {
        isAttack = false;
        int currenIndexWeapon = PlayerPrefs.GetInt(Constants.CURRENT_WEAPON_NAME);
        int currentIndexHat = PlayerPrefs.GetInt(Constants.CURRENT_HAT_NAME);
        int currentIndexPant = PlayerPrefs.GetInt(Constants.CURRENT_PANT_NAME);
        ChangeWeapon((WeaponType)currenIndexWeapon);
        ChangeHat((HatsType)currentIndexHat);
        ChangePant((PantsType)currentIndexPant);
        transform.localScale = Vector3.one;
        radius = 2f;
        horizontal = 0;
        vertical = 0;
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void Init()
    {
        isAttack = false;
        targetEnemy = Vector3.zero;
        transform.localScale = Vector3.one;
        radius = 2f;
        horizontal = 0;
        vertical = 0;
    }
    public void SetJoystick()
    {
        if (Joystick == null)
        {
            Debug.LogError("Joystick is null");
            return;
        }
        horizontal = Joystick.Horizontal;
        vertical = Joystick.Vertical;
    }

    private bool GetInPut()
    {
        SetJoystick();
        if (Joystick != null)
        {
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
            positionMove = transform.position + direction * speedMove * Time.deltaTime;
            Vector3 lookDirection = direction + playerVisual.transform.position;
            playerVisual.transform.LookAt(lookDirection);
        }
        else
        {
            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }
        transform.position = Vector3.MoveTowards(transform.position, positionMove, speedMove * Time.deltaTime);
    }

    private void LookTargetEnemy()
    {
        if (lookEnemy == null)
        {
            return;
        }
        else if (hitColliders[0] != this.gameObject)
        {
            lookEnemy.gameObject.SetActive(true);
        }
        
       // look.transform.position = lookEnemy.position;
    }

   
    protected override void Die()
    {
        base.Die();
        GameManager.Instance.ChangeState(GameState.Pasue);
        UIManager.Instance.OpenUI<CanvasFail>();
    }
}
