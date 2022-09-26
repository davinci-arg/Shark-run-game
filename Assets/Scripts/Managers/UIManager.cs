using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : PersistentSingleton<UIManager>
{
    [SerializeField] private UIMainScore _uIMainScore;

    public UIMainScore UIMainScore => _uIMainScore;

}
