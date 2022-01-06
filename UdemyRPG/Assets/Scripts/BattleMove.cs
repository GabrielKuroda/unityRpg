using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Transforma a classe em um objeto
[System.Serializable]
public class BattleMove
{
    public string moveName;
    public int movePower;
    public int moveCost;
    public AttackEffect theEffect;
}
