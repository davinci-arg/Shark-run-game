using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySharkPlace : MonoBehaviour
{
    private BabyShark _babyShark;

    public bool IsEmpty => _babyShark == null;
    public BabyShark BabyShark => _babyShark;

    public void AddBabyShark(BabyShark babyShark) => _babyShark = babyShark;

    public BabyShark GetBabyShark()
    {
        BabyShark babyShark = _babyShark;
        _babyShark = null;
        return babyShark;
    }
}
