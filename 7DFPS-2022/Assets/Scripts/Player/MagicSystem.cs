using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    public bool SystemLocked = false;

    public Transform castPoint;

    public float DamageBounus = 0;

    public Hand rightHand;
    public Hand leftHand;

    public bool cantCast = false;

    private void Start()
    {
        HUDManager.instance.UpdateManaSlider();

    }

    //void CantCastLeft()
    //{
    //    if (chargingSound)
    //    {
    //        chargingSound.Stop();
    //    }
    //    lefttHandCharge.SetBool("Charging", false);
    //    leftHandAnim.SetBool("Charging", false);

    private void Update()
    {
        if (SystemLocked)
        {
            return;
        }

        if (InputManager.Instance.IsCharging1())
        {
            rightHand.isCharging = true;
        }
        if (InputManager.Instance.IsUnReleasing1())
        {
            rightHand.isCharging = false;
            rightHand.OnRelease();
        }
        if (InputManager.Instance.IsCharging2())
        {
            leftHand.isCharging = true;
        }
        if (InputManager.Instance.IsUnReleasing2())
        {
            leftHand.isCharging = false;
            leftHand.OnRelease();
        }


    }


}

