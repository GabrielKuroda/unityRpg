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

    public int currentTurn;
    public bool turnWaiting;
    public GameObject uiButtonsHolder;

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
        //verifica se está em batalha
        if(battleActive){
            //Verifica se está em aguardando um turno
            if(turnWaiting){
                //Verifica se é o turno do PLayer
                if(activeBattlers[currentTurn].isPlayer){
                    //Ativa menu de escolha
                    uiButtonsHolder.SetActive(true);
                }else{
                    //Destaiva menu de escolha
                    uiButtonsHolder.SetActive(false);

                    //Enemy should Attack , ToDo
                }
            }

            if(Input.GetKeyDown(KeyCode.N)){
                NextTurn();
            }
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
            //Indica que está em um turno
            turnWaiting = true;
            //Indica que é o 1ºTurno
            currentTurn = 0;
        }
    }

    public void NextTurn(){
        //Add turno
        currentTurn++;
        //Verifica se passou a qtd de turnos necessarios
        if(currentTurn >= activeBattlers.Count){
            //Retorna os turnos
            currentTurn = 0;
        }
        //Indica estar em turno
        turnWaiting = true;
        //Atualiza batalha
        UpdateBattle();
    }

    public void UpdateBattle(){
        //Indicadores
        bool allEnemiesDead = true;
        bool allPlayersDead = true;
        //Passa por todos os enemies e players
        for(int i = 0; i < activeBattlers.Count; i++){
            //Verifica se o HP está abaixo de 0
            if(activeBattlers[i].currentHp < 0){
                //Indica como 0
                activeBattlers[i].currentHp = 0;
            }
            //Verifica se o HP é 0
            if(activeBattlers[i].currentHp == 0){
                //Handle dead Battler
            }else{
                //Verifica se é player ou não
                if(activeBattlers[i].isPlayer){
                    //Indica que há players vivos
                    allPlayersDead = false;
                }else{
                    //Indica que há enimies vivos
                    allEnemiesDead = false;
                }
            }
        }
        
        //Verifica se um dos lados acabou
        if(allEnemiesDead || allPlayersDead){
            //Verifica se todos os enemies morreram
            if(allEnemiesDead){
                //End battle and victory
            }else{
                //End battle failure
            }
            //Desativa batalha
            battleScene.SetActive(false);
            //Indica que não está mais em batalha
            GameManager.instance.battleActive = false;
            battleActive = false;
        }
    }
}
