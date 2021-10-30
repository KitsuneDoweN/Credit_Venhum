using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    MonsterStatus monster_hp;
    [SerializeField]
    MonsterAttack monster_attack;
    //[SerializeField]
    //MonsterAnimator monster_animator;
    [SerializeField]
    MonsterAttackSensor monster_attacksensor;
    [SerializeField]
    MonsterMoveSensor monster_movesensor;

    
    public void Init(Transform target)
    {
        //monster_animator.Init();
        monster_movesensor.Init(target, monster_attacksensor);
        monster_attack.Init(monster_hp, monster_movesensor);
    }

}
