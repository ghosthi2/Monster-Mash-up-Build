using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public AllEffects effectsPack;

    public void SpawnEffect(string name, Transform t, int dir = 1)
    {
        GameObject effect;
        GameObject effect2;
        switch (name)
        {
            case "dashf":
                effect = (GameObject)Instantiate(effectsPack.dashFwdClouds);
                effect2 = (GameObject)Instantiate(effectsPack.dashFwdSparks);
                effect.transform.position = t.position;
                effect2.transform.position = t.position;
                if(dir < 0)
                {
                    Vector3 theScale = effect.transform.localScale;
                    theScale.x *= -1;
                    effect.transform.localScale = theScale;
                    effect2.transform.localScale = theScale;
                }
                break;
            case "dashb":
                effect = (GameObject)Instantiate(effectsPack.dashBackClouds);
                effect2 = (GameObject)Instantiate(effectsPack.dashBackSparks);
                effect.transform.position = t.position;
                effect2.transform.position = t.position;
                if (dir < 0)
                {
                    Vector3 theScale = effect.transform.localScale;
                    theScale.x *= -1;
                    effect.transform.localScale = theScale;
                    effect2.transform.localScale = theScale;
                }
                break;
            case "jump":
                effect = (GameObject)Instantiate(effectsPack.jumpClouds);
                effect.transform.position = t.position;
                break;
            case "land":
                effect = (GameObject)Instantiate(effectsPack.landClouds);
                effect.transform.position = t.position;
                break;
            case "lightp":
                effect = Instantiate(effectsPack.lightPunch);
                effect.transform.position = t.position;
                break;
            case "lightk":
                effect = Instantiate(effectsPack.lightKick);
                effect.transform.position = t.position;
                break;
            case "mediump":
                effect = Instantiate(effectsPack.mediumPunch);
                effect.transform.position = t.position;
                break;
            case "mediumk":
                effect = Instantiate(effectsPack.mediumKick);
                effect.transform.position = t.position;
                break;
            case "heavyp":
                effect = Instantiate(effectsPack.heavyPunch);
                effect.transform.position = t.position;
                break;
            case "heavyk":
                effect = Instantiate(effectsPack.heavyKick);
                effect.transform.position = t.position;
                break;
            case "block":
                effect = Instantiate(effectsPack.blocked);
                effect.transform.position = t.position;
                break;
        }
    }
}
