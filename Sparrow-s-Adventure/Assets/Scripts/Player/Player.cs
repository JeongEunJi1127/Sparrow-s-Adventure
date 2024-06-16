using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerCondition Condition { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        AnimationData.AnimaterHashInit();

        Animator = GetComponentInChildren<Animator>();
        Condition = GetComponent<PlayerCondition>();
        Controller = GetComponent<PlayerController>();
        StateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        StateMachine.ChangeState(StateMachine.WalkState);
        Controller.SetSpeed(Data.PlayerGroundData.PlayerWalkSpeed);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }
}
