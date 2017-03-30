using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

public class Acorn : MonoBehaviour
{
    private Vector3 startPos;
    
    public void OnEnable()
    {
        startPos = HexCoords.Offset2Cube(HexCoords.World2Offset(transform.position));
        StartCoroutine(PlantTimer());
    }

    public void PlantAcorn()
    {
        GameController controller = GameObject.Find("GameManager").GetComponent<GameController>();
        controller.ShowViableArea(startPos,true);
        StopCoroutine("PlantTimer");
    }

    public IEnumerator PlantTimer()
    {
        int currentTime = 0;
        while (currentTime < 5)
        {
            currentTime++;
            yield return new WaitForSeconds(1f);    
        }
        //after time is up, plant this shit yourself
        List<Vector3> positions = HexCoords.HexRange(startPos,2);
        Vector3 randomPos = positions[Random.Range(0, positions.Count)];
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.PlantTree(new Pine(randomPos),HexCoords.Cube2Offset(randomPos));
        Destroy(this.gameObject);
    }
}
