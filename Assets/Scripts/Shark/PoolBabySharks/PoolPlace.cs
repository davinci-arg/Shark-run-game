using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPlace : MonoBehaviour
{
    public bool IsEmpty => _babyShark == null;

    private BabyShark _babyShark;

    public void AddBabyShark(BabyShark babyShark) => _babyShark = babyShark;


}
