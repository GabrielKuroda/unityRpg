using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public BattleMove[] movesList;
    public GameObject enemyAttackEffect;
    public DamageNumber theDamageNumber;
    public Text[] playerNames, playerHP, playerMP;
    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;
    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;
    public BattleNotification battleNotice;
    public int chanceToFlee = 35;

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

                    //Enemy Attack
                    StartCoroutine(EnemyMoveCo());
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
                            activeBattlers[i].defence = thePlayer.defence;
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
            //Atualiza os Stats do Player
            UpdateUIStats();
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
        //Atualiza os Stats do Player
        UpdateUIStats();
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
                Debug.Log("Win");
                activeBattlers.Clear();
            }else{
                //End battle failure
                Debug.Log("Loss");
                activeBattlers.Clear();
            }
            //Desativa batalha
            battleScene.SetActive(false);
            //Indica que não está mais em batalha
            GameManager.instance.battleActive = false;
            battleActive = false;
        }else{
            while(activeBattlers[currentTurn].currentHp == 0){
                currentTurn++;
                Debug.Log(activeBattlers.Count);
                if(currentTurn >= activeBattlers.Count){
                    currentTurn = 0;
                }
            }
        }
    }

    //Cria uma rotina
    public IEnumerator EnemyMoveCo(){
        //Indica não estar aguardando
        turnWaiting = false;
        //Espera 1s
        yield return new WaitForSeconds(1f);
        //Ataca
        EnemyAttack();
        //Espera 1s
        yield return new WaitForSeconds(1f);
        //Proxim turno
        NextTurn();
    }

    public void EnemyAttack(){
        //Cria uma lista só de Players
        List<int> players = new List<int>();
        //Percorre todos os Personagens na batalha
        for(int i = 0; i < activeBattlers.Count; i++){
            //Verifica se é player e se esta vivo
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHp > 0){
                //Add na lista
                players.Add(i);
            }
        }
        //Escolhe um player aleatorio
        int selectedTarget = players[Random.Range(0,players.Count)];
        //Escolhe um attack
        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        int movePower = 0;
        //Percorre a lista de attacks
        for(int i = 0; i < movesList.Length; i++){
            //Verifica se é o Attack
            if(movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack]){
                //Instancia
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }
        //Chama o Efeito de show Enemy
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        //da o dano
        DealDamage(selectedTarget,movePower);
    }

    public void DealDamage(int target, int movePower){
        //PEga a força do atacante
        float atkPwr = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower;
        //PEga a defesa do alvo
        float defPwr = activeBattlers[target].defence + activeBattlers[target].armrPower;

        //Calcula o Dano
        float damageCalc = (atkPwr / defPwr) * movePower * Random.Range(.9f,1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);
        //loga
        Debug.Log(activeBattlers[currentTurn]. charName + " is dealing " + damageCalc + "(" + damageToGive + ") damage to " + activeBattlers[target].charName);
        //Realiza o dano
        activeBattlers[target].currentHp -= damageToGive;
        //Instancia indicador de dano
        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);
        //Atuliza os Stats dos Players
        UpdateUIStats();
    }

    public void UpdateUIStats(){
        //Percorre os Labels de Stats
        for(int i = 0; i < playerNames.Length; i++){
            //Verifica se há + de um personagem na batalha
            if(activeBattlers.Count > 1){
                //Verifica se o Personagem é Player
                if(activeBattlers[i].isPlayer){
                    //Pega os Dados do Player
                    BattleChar playerData = activeBattlers[i];
                    //Set nos dados nas Labels e ativa as labels
                    playerNames[i].gameObject.SetActive(true);
                    playerNames[i].text = playerData.charName;
                    playerHP[i].text = Mathf.Clamp(playerData.currentHp, 0 , int.MaxValue) +"/"+ playerData.maxHp;
                    playerMP[i].text = playerData.currentMP +"/"+ playerData.maxMP;
                }else{
                    //Desativa as labels
                    playerNames[i].gameObject.SetActive(false);
                }
            }else{
                //Desativa as Labels
                playerNames[i].gameObject.SetActive(false);
            }
        }
    }

    public void PlayerAttack(string moveName, int selectedTarget){
        int movePower = 0;
        //Percorre a lista de attacks
        for(int i = 0; i < movesList.Length; i++){
            //Verifica se é o Attack
            if(movesList[i].moveName == moveName){
                //Instancia
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }
        //Instancia o Efeito no Player
        Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        //Da dano
        DealDamage(selectedTarget, movePower);
        //Desativa o Menu de Target
        targetMenu.SetActive(false);
        //Chama proximo turno
        NextTurn();
    }

    public void OpenTargetMenu(string moveName){
        //Abre menu de target
        targetMenu.SetActive(true);
        //Cria uma lista para armazenar os enemies
        List<int> enemies = new List<int>();
        //Percorre os personagens em batalha
        for(int i = 0; i < activeBattlers.Count; i++){
            //Verifica se é Enemy
            if(!activeBattlers[i].isPlayer){
                enemies.Add(i);
            }
        }
        //Percorre os botões de target
        for(int i = 0; i < targetButtons.Length; i++){
            //Verifica se o deve criar o botão, com base na qtd de enemies
            if(enemies.Count > i){
                //Cria o botão
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattlerTarget = enemies[i];
                targetButtons[i].targetName.text = activeBattlers[enemies[i]].charName;
            }else{
                //Desativa o botão
                targetButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenMagicMenu(){
        //Ativa o menu de magic
        magicMenu.SetActive(true);
        //Percorre os botões
        for(int i = 0; i < magicButtons.Length; i++){
            //Verifica se o deve criar o botão, com base na qtd de magic do player
            if(activeBattlers[currentTurn].movesAvailable.Length > i){
                //Cria o botão
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;
                //Percorre as Abilidades presentes no Jogo
                for(int j = 0; j < movesList.Length; j++){
                    //Verifica se a abilidade bate com a do Player
                    if(movesList[j].moveName == magicButtons[i].spellName){
                        //Set no Cost da MAgic
                        magicButtons[i].spellCost = movesList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }else{
                //Desativa a tela de Magic
                magicButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void Flee(){
        //Pega um numero entre 0 - 100
        int fleeSuccess = Random.Range(0,100);
        //Verifica se o numero está na porcentagem
        if(fleeSuccess < chanceToFlee){
            //Termina a Batalha
            battleActive = false;
            battleScene.SetActive(false);
        }else{
            //Notifica que não conseguiu escapar
            battleNotice.theText.text = "Couln't Escape!";
            battleNotice.Activate();
            //Chama proximo turno
            NextTurn();
        }
    }
}
