using UnityEngine;
using System.Collections;

public class DisableNoAdds : MonoBehaviour {
    public void Awake()
    {
#if NO_ADDS_APLICATION
        gameObject.SetActive(false);
#endif
    }

}
