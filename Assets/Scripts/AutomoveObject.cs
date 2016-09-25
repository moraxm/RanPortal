using UnityEngine;
using System.Collections;

public class AutomoveObject : MonoBehaviour
{

    public ISpeedSource speedSource
    {
        get;
        set;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (speedSource != null)
            transform.position += Vector3.down * speedSource.speed * Time.deltaTime;
    }
}
