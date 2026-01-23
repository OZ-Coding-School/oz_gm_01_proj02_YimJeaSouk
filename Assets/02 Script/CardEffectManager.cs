using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CardEffectManager : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private List<GameObject> provisoButtons;
    [SerializeField] private List<provisoButton> buttonscript;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject smorke;
    [SerializeField] private List<RawImage> smorks;
    [SerializeField] private RawImage fullsmork;
    [SerializeField] private CardPlacementManger cardPlacement;
    [SerializeField] private SoundManager sound;

    private CardSlot currentSlot;
    private GameObject currentCardBack;

    public CardSlot useCurrentSlot;
    public GameObject useCurrentCardback;
    


    public void activate(string name)
    {
        Effect(name);
    }

    private void Effect(string effectName)
    { // 각 종류의 카드 효과를 일괄 관리
        switch(effectName)
        {
            case "방탄조끼":
                BulletProof();
                break;
            case "최면가스":
                HypnoticGas();
                break;
            case "아무것도없는방":
                
                break;
            case "은열쇠":
                
                break;
            case "살인마의함정":
                KillersTrap();
                break;
            case "작은단서":
                LittleProviso();
                break;
            case "탈출구":
                
                break;

        }
    }

    private void BulletProof()
    {
        //다른곳에서 작성되었다. 없어도 되는 메서드 다만 설명용 텍스트가 뜨도록 할 예정이라면 내버려두자.
       
    }
    private void HypnoticGas()
    {
        //최면가스 : 기능 자체는 이미 되어있음 여기에 연결만 하면 됨.
        StartCoroutine(waitImage());
        
    }
    private IEnumerator waitImage()
    {
        yield return new WaitForSeconds(1f);
        sound.UsePlaySound(1);
        smorke.SetActive(true);
        foreach (RawImage smorges in smorks)
        {
            smorges.DOFade(1f, 2f);
        }
        fullsmork.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        fullsmork.DOFade(1f, 1f).OnComplete(() =>
        {
            cardPlacement.useRePlaceMent();
            fullsmork.DOFade(0f, 1f);

        });
        yield return new WaitForSeconds(2f);
        foreach (RawImage smorges in smorks)
        {
            smorges.DOFade(0f, 1f);
        }
        yield return new WaitForSeconds(2f);
        smorke.SetActive(false);
        fullsmork.gameObject.SetActive(false);

    }
    
    private void KillersTrap()
    {
        gameManager.UseGameover();
       
    }
    
    private void LittleProviso()
    {
        shild.gameObject.SetActive(true);

        StartCoroutine(provisoActive());

       
    }
    
    public void UseprovisoDeactive()
    {
        provisoDeactive();
    }
    private void provisoDeactive()
    {
        foreach (GameObject buttons in provisoButtons)
        {
           
            buttons.SetActive(false);
        }
        shild.gameObject.SetActive(false);
    }
    public void UseprovisoNotSee()
    {
        provisoNotSee();
    }
    private void provisoNotSee()
    {
        foreach (GameObject buttons in provisoButtons)
        {
            
            buttons.GetComponent<Image>().DOFade(0f,0.2f);
        }
    }
    private IEnumerator provisoActive()
    {
        yield return new WaitForSeconds(1.05f);
        shild.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        
        for (int i = 0; i < buttonscript.Count; i++)
        {
            buttonscript[i].UseactiveButton();
        }
        
    }
}
