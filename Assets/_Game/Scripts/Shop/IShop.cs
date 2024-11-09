using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AShop: UICanvas
{
    [SerializeField]
    protected Button btnPrev;
    [SerializeField]
    protected Button btnNext;
    [SerializeField]
    protected Button bntBuy;
    [SerializeField]
    protected Button btnTake;
    [SerializeField]
    protected Character character;
    protected int currentItem = 0;
    protected int maxNumberOfData;
    public void OnInit(int maxNumber)
    {
        maxNumberOfData = maxNumber;
    }
    public void ChangePrev()
    {
        if(currentItem > 0)
        {
            currentItem--;
            ChangeItem();
        }
    }
    public void ChangeNext()
    {
        if(currentItem < maxNumberOfData - 1)
        {
            currentItem++;
            ChangeItem();
        }
    }
    public abstract void ChangeItem();
    public abstract void BuyItem();
    public abstract void TakeItem();
    public override void Open()
    {
        base.Open();
        btnNext.onClick.AddListener(ChangeNext);
     
    }
}