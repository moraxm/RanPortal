using UnityEngine;
using System.Collections;

public class AutomoveObject : MonoBehaviour
{

    public ISpeedSource speedSource
    {
        get;
        set;
    }

    public float m_size = 5;
    public float size
    {
        get { return m_size; }
    }

    LaneObject m_laneObject;
    public LaneObject laneObject
    {
        get { return m_laneObject; }
    }

    // Use this for initialization
    public virtual void Awake()
    {
        m_laneObject = GetComponent<LaneObject>();
        dontDestroy = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (speedSource != null)
            transform.position += Vector3.down * speedSource.speed * Time.deltaTime;
    }

    public bool dontDestroy { get; set; }
}
