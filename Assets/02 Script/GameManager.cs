
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardPlacementManger cardPlacement;
    [SerializeField] private CardEffectManager cardEffect;
    [SerializeField] private List<CardSlot> slots;
    [SerializeField] private List<CardButton> Buttons;
    [SerializeField] private int ReversCount;
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private UnityEngine.UI.RawImage Blood;
    [SerializeField] private TMP_Text gameOver;
    [SerializeField] private TMP_Text gameClear;

    //[SerializeField] private bool isGameOver = false;
    //[SerializeField] private bool isGameClear = false;

    private bool bulletProof = false;




    private void Start()
    {
        cardPlacement.useLogic();
    }
   
    public void UseRestart()
    {
        ReStartGame();
    }

    public void UseGameover()
    {
        GameOver();
    }
    private void GameOver()
    {
        BulletProofUpdate(); // 방탄 조끼 작동여부 업데이트
        if (bulletProof == false)
        {
            // 작동중이 아니라면, 게임오버 절차
            Blood.gameObject.transform.localScale = Vector3.one;
            Blood.gameObject.SetActive(true);
            Blood.gameObject.transform.DOScale(1.2f, 0.05f).OnComplete(() =>
            {
                gameOver.alpha = 0f;
                gameOver.gameObject.SetActive(true);
                gameOver.DOFade(1f, 1f).SetEase(Ease.InCirc);
            });
            
            
        }
        else if(bulletProof == true)
        {
            //방탄조끼 작동중 이라면 게임오버 안됨.
            bulletProof = false;
            cardPlacement.UesOperationBulletproof();
            
        }
    }
    public void UseGameclaer()
    {
        GameClear();
    }
    private void GameClear() 
    {
        gameClear.alpha = 0f;
        gameClear.gameObject.SetActive(true);
        gameClear.DOFade(1f, 1f).SetEase(Ease.InCirc);
    }
    private void ReStartGame()
    {
        gameClear.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        Blood.gameObject.SetActive(false);
        cardPlacement.UseRestartingReady();
    }

    private void BulletProofUpdate()
    {
        cardPlacement.ApplieBulletproof(); // 방탄조끼 작동여부 내보내기 위한 업데이트 작업
        bulletProof = cardPlacement.useBulletProof; // 방탄조끼 여부를 bulletProof 에 복사해넣기
    }


}
