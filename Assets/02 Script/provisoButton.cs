using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class provisoButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button ProvisoButton;
    [SerializeField] private CardPlacementManger cardPlacement;
    [SerializeField] private CardEffectManager cardEffect;
    [SerializeField] private UnityEngine.UI.RawImage shild;
    [SerializeField] private UnityEngine.UI.RawImage shild2;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject cardback;
    [SerializeField] private GameObject imagecard;
    [SerializeField] private Image cardBack;
    [SerializeField] private SoundManager sound;



    public void UseFor()
    {
        shild2.gameObject.SetActive(true);
        cardEffect.UseprovisoNotSee();
        ProvisoShowing();
    }
    private void ProvisoShowing()
    {
        sound.UsePlaySound(10);
        cardBack.DOFade(0.5f, 0.5f).OnComplete(()=>
        {
            StartCoroutine(showproviso());
        });

    }

    public void UseactiveButton()
    {
        activeButton();
    }
    private void activeButton()
    {
        if(cardback.gameObject.activeSelf)
        {
            ProvisoButton.gameObject.SetActive(true);
            ProvisoButton.GetComponent<Image>().DOFade(0.25f, 0.2f).OnComplete(()=>
            {
                sound.UsePlaySound(2);
            });
        }
    }

    private IEnumerator showproviso()
    {
        
        yield return new WaitForSeconds(3);
        cardBack.DOFade(1f, 0.5f).OnComplete(() =>
        {
            cardEffect.UseprovisoDeactive();
        });
    }
}
