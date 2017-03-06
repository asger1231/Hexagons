using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexes;

public class HexagonVisualizer : MonoBehaviour {

    public float hexSize = 1f;
    public Vector2 position = Vector2.zero;
    public bool pointyTop = true;
    public Color hexColor = Color.yellow;

    Hexagon hex;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ResetHexValues() {
        hex.position = position;
        hex.pointy = pointyTop;
        hex.size = hexSize;
    }

    void OnDrawGizmos() {
        if (hex == null)
            hex = new Hexagon(transform.position, pointyTop, hexSize);

        ResetHexValues();

        Gizmos.color = hexColor;
        for (int i = 0; i < 6;)
            Gizmos.DrawLine(hex.GetCornerGlobalPosition(i), hex.GetCornerGlobalPosition(++i));
    }
}
