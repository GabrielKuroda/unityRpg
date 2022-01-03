using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance;

    private bool battleActive;
    public GameObject battleScene;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    public BattleChar[] playerPrefabs;
    public BattleChar[] enemyPrefabs;
    public List<BattleChar> activeBattlers = new List<BattleChar>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            BattleStart(new string[] {"Eyeball","Spider","Skeleton"});
        }
    }

    public void BattleStart(string[] enemiesToSpawn){
        //Verifica se está em batalha
        if(!battleActive){
            //Informa estar em Batalha
            battleActive = true;
            GameManager.instance.battleActive = true;
            //Set na posição do BG
            transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,transform.position.z);
            //Ativa cena de Batalha
            battleScene.SetActive(true);
            //Toca musica
            AudioManager.instance.PlayBgm(0);
            //PErcorre as posições de batalha
            for(int i = 0; i < playerPositions.Length;i++){
                //Verifica se o Player está ativo
                if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy){
                    //Percorre os Chars
                    for(int j = 0; j < playerPrefabs.Length; j++){
                        //Verifica se é o Char
                        if(playerPrefabs[j].charName == GameManager.instance.playerStats[i].charName){
                            //Instancia o Char na Cena
                            BattleChar newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
                            //Set no pai do char como a posição
                            newPlayer.transform.parent = playerPositions[i];
                            //Add na Lista
                            activeBattlers.Add(newPlayer);
                            //Add status do char na lista
                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHp = thePlayer.currentHP;
                            activeBattlers[i].maxHp = thePlayer.maxHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maxMP = thePlayer.maxMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defense = thePlayer.defence;
                            activeBattlers[i].wpnPower = thePlayer.wpnPwr;
                            activeBattlers[i].armrPower = thePlayer.armrPwr;
                        }
                    }
                }
            }
            //Percorre os Enemies que vão spawnar
            for(int i = 0; i < enemiesToSpawn.Length; i++){
                //Verifica se não está vazio
                if(enemiesToSpawn[i] != ""){
                    //Percorre os enemies do Jogo
                    for(int j = 0; j < enemyPrefabs.Length; j++){
                        //Se for o Enemy
                        if(enemyPrefabs[j].charName == enemiesToSpawn[i]){
                            //Instancia
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].transform.position, enemyPositions[i].transform.rotation);
                            //Set no pai do char como a posição
                            newEnemy.transform.parent = enemyPositions[i];
                            //Add na Lista
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
        }
    }
}
