using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    GreatBritain,
    Germany
}

public class ChooseFaction : MonoBehaviour
{
    public Faction faction;

    public void onFactionChosen()
    {
        switch (faction)
        {
            case Faction.GreatBritain:
                FactionSelectionManager.Instance.onPlayerFactionChosen(faction);
                break;
            case Faction.Germany:
                FactionSelectionManager.Instance.onPlayerFactionChosen(faction);
                break;
        }
    }
}
