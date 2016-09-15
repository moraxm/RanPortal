using UnityEngine;
using System.Collections;

public class MotorScreen : MonoBehaviour {

	public GameObject motorScreen;
	public GameObject contenedorCalles;
    public LaneGenerator laneGenerator;

	public float speed;

    public int numSelectorDeCalle;
	public int contadorCalles = 0;

	public bool cuentaRegresivaTermino;
	public bool juegoTerminado;

	// Use this for initialization
	void Start () {
		juegoTerminado = false;
        GenerateLanes();
	}

    private void GenerateLanes()
    {
        float offsetY = 0;
        // Primera calle, vacía
        GameObject firstLane = laneGenerator.getFirstLane();
        firstLane.transform.parent = motorScreen.transform;
        offsetY += firstLane.GetComponent<Lane>().length * 0.5f;

        // Al menos 3 calles más
        for (int i = 0; i < 3; ++i)
        {
            GameObject lane = laneGenerator.getLane();
            lane.transform.parent = motorScreen.transform;
            Vector3 pos = Vector3.zero;
            pos.y = offsetY + firstLane.GetComponent<Lane>().length * 0.5f;
            offsetY += firstLane.GetComponent<Lane>().length;
            lane.transform.localPosition = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
		motorScreen.transform.Translate(Vector3.down * speed * Time.deltaTime);
	    
	}
}
