using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : UIInteractBase
{
    [SerializeField] private GameObject _selectMenu;
    [SerializeField] private Sprite _selectFonts;

    [Space(10.0f)]
    [SerializeField] private Sprite _originFonts;

    private Image _thisImg;

    private void Awake()
    {
        _thisImg = GetComponent<Image>();
    }

    public override void OnPointerEnter()
    {
        if (_thisImg != null)
        {
            _thisImg.sprite = _selectFonts;
        }

        print("enter");
        _selectMenu.SetActive(true);
    }

    public override void OnPointerExit()
    {
        if (_thisImg != null)
        {
            _thisImg.sprite = _originFonts;
        }

        print("exit");
        _selectMenu.SetActive(false);
    }

    public void OnClickCampaignBtn()
    {
        int stageNumber = int.Parse(gameObject.name.Split(" ")[0]);
        switch (stageNumber)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
