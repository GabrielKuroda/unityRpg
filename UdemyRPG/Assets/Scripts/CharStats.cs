using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel =1;
    public int currentEXP;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;
    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int defence;
    public int wpnPwr;
    public int armrPwr;
    public string equippedWpn;
    public string equippedArmr;
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        //Popula Xp para cada Level
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for(int i = 2;i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i-1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp(int expToAdd){
        //Adiciona Xp
        currentEXP += expToAdd;

        //Verifica se ja atingiu o Lvl Maximo
        if(playerLevel < maxLevel){
            //Verifica se pode subir de nivel
            if(currentEXP > expToNextLevel[playerLevel])
            {
                //Utiliza o Xp necessario e sobe de nivel
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;

                //Determina qual status adicionar Str ou Def baseado em par ou impar
                if(playerLevel%2 == 0)
                {
                    strength++;
                }else{
                    defence++;
                }

                //Aumente Hp maximo
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);

                //Restora HP
                currentHP = maxHP;

                //Adiciona MP
                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }
        //Limpa o Xp caso estaja no Max lvl
        if(playerLevel >= maxLevel){
            currentEXP = 0;
        }
    }

}
