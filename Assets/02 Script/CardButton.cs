using DG.Tweening;
using UnityEngine;

public class CardButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button playButton;
    [SerializeField] private CardPlacementManger cardPlacementManger;
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    private Tween ScaleTween;

    public void UseFor()
    {
        cardPlacementManger.currentButton = this;
        shild.gameObject.SetActive(true);
        ButtonDeactivation();
    }
    public void UseRevers()
    { //다시 뒤집혀 뒷면이 되도록 하기
     
        ButtonRevers();
    }
    private void ButtonDeactivation()
    {
       ScaleTween = transform.DOScaleX(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            

            // 이것이 완료 될때까지. 앞에 투명 패널로 가려, 버튼이 안눌리도록 UseFor에서 shild 를 SetAciive(true) 함.
        });
    }
    private void ButtonRevers()
    {
       
        gameObject.SetActive(true);
        transform.localScale = new Vector3(0,1,1);
        
        transform.DOScaleX(1, 0.5f).OnComplete(() =>
        {
        shild2.gameObject.SetActive(false);
        });
        
    }
}
