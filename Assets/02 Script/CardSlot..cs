
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class CardSlot : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private int cardNum;
    [SerializeField] private CardPlacementManger cardPlacementManger;
    [SerializeField] private CardEffectManager cardEffectManager;
    [SerializeField] private SoundManager sound;
    [SerializeField] private int keyCount;
  
    

    public void Useindata(string name)
    {
        Indata(name);
    }
    private void Indata(string name)
    {
        cardName = name;
    }

    public void callingcardOut()
    { //카드뒷면버튼용.
        CardCall(cardName);
        Cardpush();
    }

    public void CallingProvisouOut()
    {//단서버튼용
        CardCall(cardName);
        provisocalling();
    }
 
    public void CallKeyCount()
    {
        cardPlacementManger.UseingKeycount();
        keyCount = cardPlacementManger.usekeyCount ;
    }
    private void ExitCard()
    {
        
        CallKeyCount();
        if (keyCount == 3)
        {
            
            cardNum = 6;
        }
        else
        {
            
            cardNum = 5;
        }
    }

    private void CardCall(string name)
    {
        
        cardPlacementManger.currentSlot = this;
        switch (name)
        {
            case "방탄조끼":
                
                cardNum = 0;
                cardPlacementManger.currentbulletProofSlot = this;
                break;
            case "최면가스":
                cardNum = 1;
                break;
            case "아무것도없는방":
                cardNum = 5;
                break;
            case "은열쇠":
                cardNum = 3;
                // 얻은 카드 제외용
                break;
            case "살인마의함정":
                cardNum = 2;
                break;
            case "작은단서":
                cardNum = 4;
                break;
            case "탈출구":
                ExitCard();
                break;

        }
        
            
    }
    public void UsecaingeCard(string cardName)
    {
        caingeCard(cardName);
    }
    private void caingeCard(string name)
    {
        cardName = name;
    }
    private void provisocalling()
    {
        cardPlacementManger.UseLittelProviso(cardNum);
    }
    private void Cardpush()
    {
        cardPlacementManger.CallingMoveCardImage(cardNum);
        cardEffectManager.activate(cardName);
    }
    

  
}
