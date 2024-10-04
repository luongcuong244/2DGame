using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    // Phương thức abstract mà các lớp kế thừa bắt buộc phải cài đặt
    public abstract Sprite GetImage();
}