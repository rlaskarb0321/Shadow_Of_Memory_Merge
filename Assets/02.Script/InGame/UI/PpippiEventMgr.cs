using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// 이 스크립트에서 newList, oldList 값 처리를 해야한다.
public class PpippiEventMgr : MonoBehaviour
{
    [Header("=== Event Item ===")]
    public GameObject _newEventItem;
    public GameObject _oldEventItem;
    public Dropdown _orderDropDown;
    [Space(5.0f)] [SerializeField] private CampaignUI _campaignUI;

    [Header("=== Ppippi Alarm ===")]
    public GameObject _ppippiAlarm;

    [Space(10.0f)] [SerializeField] private GameObject _eventListPrefabs;
    private enum eOrderBy { IndexUp, IndexDown, NameUp, NameDown, NotWatching, Watching }
    public List<PpippiEvent> _ppippiOldEventList;

    private void Start()
    {
        _ppippiOldEventList = new List<PpippiEvent>();
    }

    public void CreateNewList(PpippiEventData data)
    {
        if (!_ppippiAlarm.activeSelf)
        {
            _ppippiAlarm.SetActive(true);
        }

        PpippiEvent eventList = Instantiate(_eventListPrefabs, Vector3.zero, Quaternion.identity).GetComponent<PpippiEvent>();

        // 강조 Event 항목에 값이 이미 있다면, 이미 있던 값을 oldEvent 항목으로 옮기고, 새로 들어온 값이 강조 Event로 들어간다.
        if (_newEventItem.transform.childCount != 0)
        {
            PpippiEvent currNewEvent = _newEventItem.transform.GetChild(0).GetComponent<PpippiEvent>();

            currNewEvent.SetParentObj(_oldEventItem.transform, PpippiEvent.eMyParentObj.Old, _campaignUI, this);
            eventList.SetEventValue(data);
            eventList.SetParentObj(_newEventItem.transform, PpippiEvent.eMyParentObj.New, _campaignUI, this);
        }
        // 강조 Event 항목에 값이 없다면, 새로 들어온 값이 강조 Event로 들어간다.
        else
        {
            eventList.SetEventValue(data);
            eventList.SetParentObj(_newEventItem.transform, PpippiEvent.eMyParentObj.New, _campaignUI, this);
        }

        // 옮기면서, 정렬 기준값을 참조하여 재 정렬후 나열한다.
        OrderByDropDownValue();
    }

    public void OrderByDropDownValue()
    {
        List<PpippiEvent> ppippiEvents = _ppippiOldEventList.ToList();

        switch ((eOrderBy)_orderDropDown.value)
        {
            case eOrderBy.IndexUp:
                _ppippiOldEventList = _ppippiOldEventList.OrderBy(x => x._idx).ToList();
                for (int i = 0; i < _ppippiOldEventList.Count; i++)
                {
                    for (int j = 0; j < ppippiEvents.Count; j++)
                    {
                        if (ppippiEvents[j]._idx.Equals(_ppippiOldEventList[i]._idx))
                        {
                            ppippiEvents[j].transform.SetSiblingIndex(i);
                            break;
                        }
                    }
                }
                break;

            case eOrderBy.IndexDown:
                _ppippiOldEventList = _ppippiOldEventList.OrderByDescending(x => x._idx).ToList();
                for (int i = 0; i < _ppippiOldEventList.Count; i++)
                {
                    for (int j = 0; j < ppippiEvents.Count; j++)
                    {
                        if (ppippiEvents[j]._idx.Equals(_ppippiOldEventList[i]._idx))
                        {
                            ppippiEvents[j].transform.SetSiblingIndex(i);
                            break;
                        }
                    }
                }
                break;

            case eOrderBy.NameUp:
                _ppippiOldEventList = _ppippiOldEventList.OrderBy(x => x._name).ToList();
                for (int i = 0; i < _ppippiOldEventList.Count; i++)
                {
                    for (int j = 0; j < ppippiEvents.Count; j++)
                    {
                        if (ppippiEvents[j]._idx.Equals(_ppippiOldEventList[i]._name))
                        {
                            ppippiEvents[j].transform.SetSiblingIndex(i);
                            break;
                        }
                    }
                }
                break;

            case eOrderBy.NameDown:
                _ppippiOldEventList = _ppippiOldEventList.OrderByDescending(x => x._name).ToList();
                for (int i = 0; i < _ppippiOldEventList.Count; i++)
                {
                    for (int j = 0; j < ppippiEvents.Count; j++)
                    {
                        if (ppippiEvents[j]._idx.Equals(_ppippiOldEventList[i]._name))
                        {
                            ppippiEvents[j].transform.SetSiblingIndex(i);
                            break;
                        }
                    }
                }
                break;

            case eOrderBy.NotWatching:
                break;

            case eOrderBy.Watching:
                break;
        }

    }
}