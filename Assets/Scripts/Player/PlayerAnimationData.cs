using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] string groundParameterName = "Ground";
    [SerializeField] string idleParameterName = "Idle";
    [SerializeField] string walkParameterName = "Walk";
    [SerializeField] string runParameterName = "Run";

    [SerializeField] string airParameterName = "Air";
    [SerializeField] string jumpParameterName = "Jump";
    [SerializeField] string fallParameterName = "Fall";

    //[SerializeField] string attackParameterName = "Attack";
    //[SerializeField] string baseAttackParameterName = "BaseAttack";

    public int GroundParameterHash {  get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    //public int AttackParameterHash { get; private set; }
    //public int BaseAttackParameterHash { get; private set; }


    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);

        //AttackParameterHash = Animator.StringToHash(attackParameterName);
        //BaseAttackParameterHash = Animator.StringToHash(baseAttackParameterName);
    }
}
