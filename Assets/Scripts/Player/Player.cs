using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }

    private PlayerStateMachine stateMachine;
    public ForceHandler ForceHandler { get; private set; }
    


    void Awake()
    {
        AnimationData.Initialize();
        
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        ForceHandler = GetComponent<ForceHandler>();

        stateMachine = new PlayerStateMachine(this);
    }


    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }


    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }


    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
