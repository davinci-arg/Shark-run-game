using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySharkPlace : MonoBehaviour
{
    public bool IsEmpty => _babyShark == null;

    private BabyShark _babyShark;

    public void AddBabyShark(BabyShark babyShark) => _babyShark = babyShark;

    public BabyShark GetBabyShark()
    {
        BabyShark babyShark = _babyShark;
        _babyShark = null;
        return babyShark;
    }
}
