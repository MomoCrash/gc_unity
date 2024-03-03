using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{

    public AnimationCurve curve;

    public Light2D Sun;

    public float Steps = 1000;
    private float progression = 0;

    private void Start()
    {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            print(progression);
            print(progression / Steps);
            progression++;
            Sun.intensity = curve.Evaluate(progression / Steps);
            if (progression >= Steps)
            {
                progression = 0;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
