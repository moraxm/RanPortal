using UnityEngine;
using System.Collections;

public class OpenLink : MonoBehaviour {

    public void Open(string link)
    {
        Application.OpenURL ("market://search?q="+link);
    }
}
