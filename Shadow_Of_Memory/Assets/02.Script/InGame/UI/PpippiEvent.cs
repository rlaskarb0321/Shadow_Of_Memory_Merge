using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PpippiEvent : MonoBehaviour
{
    [SerializeField] private Text _eventName;
    [SerializeField] private Text _eventIdx;
    [SerializeField] private Text _isWatchingText;
    public ppippiEventData _eventData;

    public enum eMyParentObj { New, Old, };
    private eMyParentObj _pObj; // �� ��ũ��Ʈ�� ������ ����Ʈ�� new, old �� �Ǵ��ϱ� ���� ����
    private CampaignUI _campaignUI; // ui�г��� ��/���� ������ ���� ��ü ���޹��� ����
    private PpippiEventMgr _eventMgr; // ����Ʈ ���� & �߻� �Ӹ��� �˶� ������ ���� ��ü ���޹��� ����

    public void SetEventValue(ppippiEventData data)
    {
        _eventData = data;

        _eventIdx.text = data._idx.ToString();
        _eventName.text = data._name;
        _isWatchingText.text = ConstData._isNotWatching;
    }

    public void SetParentObj(Transform tr, eMyParentObj pObj, CampaignUI campaignUI = null, PpippiEventMgr eventMgr = null)
    {
        if (campaignUI != null && _campaignUI == null)
            _campaignUI = campaignUI;

        if (eventMgr != null && _eventMgr == null)
            _eventMgr = eventMgr;

        if (pObj.Equals(eMyParentObj.Old))
            _eventMgr._ppippiOldEventList.Add(this);

        transform.SetParent(tr);
        transform.localPosition = Vector3.zero;
        _pObj = pObj;
    }

    public void OnClickEventList()
    {
        if (_pObj.Equals(eMyParentObj.New))
        {
            // ������ Ŭ���Ȱ���, �߻��� �˶��� �� �ְ� ���ĵ� ���Ѿ��Ѵ�.
            _eventMgr._ppippiAlarm.SetActive(false);
            this.SetParentObj(_eventMgr._oldEventItem.transform, eMyParentObj.Old);
        }

        // ��ȭ ui ���ְ�, �߻��̺�Ʈ ui ���ְ�
        _campaignUI.SetDialogOn(true, _eventData._fileName);
        _campaignUI.SetPpippiEventActive(false);
        _eventData._isWatching = true;
        _isWatchingText.text = ConstData._isWatching;
    }
}