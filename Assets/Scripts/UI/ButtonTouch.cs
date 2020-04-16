using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTouch : MonoBehaviour
{
    [SerializeField] private Button _atackZoneGreen;
    [SerializeField] private Button _atackZoneYellow;
    [SerializeField] private Button _atackZoneRed;
    [SerializeField] private Button _trigger;

    public void Init(GameObject squad)
    {
        _trigger.onClick.AddListener(squad.GetComponent<SquadControlPanel>().FireCommand);
        _atackZoneGreen.onClick.AddListener(() => squad.GetComponent<SquadControlPanel>().ChangeAttackZone(2));
        _atackZoneYellow.onClick.AddListener(() => squad.GetComponent<SquadControlPanel>().ChangeAttackZone(1));
        _atackZoneRed.onClick.AddListener(() => squad.GetComponent<SquadControlPanel>().ChangeAttackZone(0));
    }

    public void OnDisable()
    {
        _trigger.onClick.RemoveAllListeners();
        _atackZoneGreen.onClick.RemoveAllListeners();
        _atackZoneYellow.onClick.RemoveAllListeners();
        _atackZoneRed.onClick.RemoveAllListeners();
    }
}
