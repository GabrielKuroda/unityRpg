using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;
    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;
    public GameObject[] statusButtons;
    public Text statusName, statusHp,statusMp,statusStr,statusDef,statusWpnEqpd,statusWpnPwr,statusArmrEqpd,statusArmrPwr,statusExp;
    public Image statusImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se botão foi apertado para abrir/fechar menu
        if(Input.GetButtonDown("Fire2") && !GameManager.instance.fadingBetweenAreas 
            && !GameManager.instance.dialogActive){
            if(theMenu.activeInHierarchy){
                //Função para fechar o menu
                CloseMenu();
            }else{
                //Abre Menu
                theMenu.SetActive(true);
                //Atualiza os Status do Player
                updateMainStats();
                //Indica que o Menu está aberto
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void updateMainStats(){
        //Pega os Valores do Status da Clasee GameManager
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++){
            //Verifica se o Personagem está ativo
            if(playerStats[i].gameObject.activeInHierarchy){
                //Ativa o personagem no menu
                charStatHolder[i].SetActive(true);
                //Set nome no Menu
                nameText[i].text = playerStats[i].charName;
                //Set Hp no Menu
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                //Set Mp no Menu
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                //Set Level no Menu
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                //Set texto Exp
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                //Set Valor Maximo Slider
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                //Set Valor do Slider
                expSlider[i].value = playerStats[i].currentEXP;
                //Set Imagem no Menu
                charImage[i].sprite = playerStats[i].charImage;
            }else{
                //Desativa o personagem no Menu
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber){
        //Atualiza os Status na Main
        updateMainStats();
        //Percorre o Array de janelas
        for(int i = 0; i < windows.Length; i++){
            //Verifica se a Janela corresponde com o botão
            if(i == windowNumber){
                //Abre ou fecha a Janela dependendo do Status atual dela
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }else{
                //Fecha as outras Janelas
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu(){
        //Percorre o Array de janelas
        for(int i = 0; i < windows.Length; i++){
            //Fecha a Janela
            windows[i].SetActive(false);
        }
        //Desativa o Menu
        theMenu.SetActive(false);
        //Indica que o Menu não está aberto
        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus(){
        //Atualiza os Status na Main
        updateMainStats();
        //Atualiza as informações de Status
        StatusChar(0);
        //Atualiza os Botões
        for(int i = 0; i < statusButtons.Length; i++){
            //Ativa e Desativa os botões
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            //Set no nome do char
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selected){
        //Preenche a tela de status
        statusName.text = playerStats[selected].charName;
        statusHp.text = ""+ playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMp.text = ""+ playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if(playerStats[selected].equippedWpn != ""){
            statusWpnEqpd.text = playerStats[selected].equippedWpn;
        }
        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
        if(playerStats[selected].equippedArmr != ""){
            statusArmrEqpd.text = playerStats[selected].equippedArmr;
        }
        statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }
}
