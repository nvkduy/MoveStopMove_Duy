using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : UICanvas
{
    [SerializeField] Button btnHats;
    [SerializeField] Button btnPants;
    [SerializeField] Button btnBack;

    public override void Setup()
    {
        base.Setup();
        btnHats.onClick.AddListener(HatsButton);
        btnPants.onClick.AddListener (PantsButton);
        btnBack.onClick.AddListener (BackButton);
    }
    private void HatsButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasHatsShop>();
    }

    private void PantsButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasPantsShop>();
    }

    private void BackButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShop>();
    }
}
