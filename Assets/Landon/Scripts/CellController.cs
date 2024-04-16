using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] Material visitedMaterial;

    public bool isVisited;
    public int row;
    public int col;

    private void Start()
    {
        isVisited = false;
    }

    public void VisitCell()
    {
        isVisited = true;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = visitedMaterial;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public GameObject[] GetUnvisitedNeighbors(GameObject[][] cells)
    {
        GameObject[] neighbors = GetAllNeighbors(cells);
        List<GameObject> unvisited = new List<GameObject>();
        foreach (GameObject cell in neighbors)
        {
            CellController cg = cell.GetComponent<CellController>();
            if(!cg.isVisited)
            {
                unvisited.Add(cell);
            }
        }
        return unvisited.ToArray();
    }

    private GameObject[] GetAllNeighbors(GameObject[][] cells)
    {
        List<GameObject> neighbors = new List<GameObject>();
        for (int i = -1; i < 2; i += 2)
        {
            try
            {
                neighbors.Add(cells[row + i][col]);
            }
            catch (IndexOutOfRangeException) { }

            try
            {
                neighbors.Add(cells[row][col + i]);
            }
            catch (IndexOutOfRangeException) { }
        }

        return neighbors.ToArray();
    }
}
