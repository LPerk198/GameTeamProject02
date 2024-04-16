using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] int mazeHeight;
    [SerializeField] int mazeWidth;
    [SerializeField] float waitTime;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject cellContainer;

    private GameObject[][] cells;

    void Start()
    {
        cells = new GameObject[mazeHeight][];
        generateCells();
        ScaleFloor();
        if(waitTime > 0)
        {
            StartCoroutine(iterativeDepthFirstSearch(cells[0][0]));
        } else
        {
            generateRoute(cells[0][0]);
        }
    }

    private void generateCells()
    {
        for(int i = 0; i < mazeHeight; i++)
        {
            GameObject[] row = new GameObject[mazeWidth];
            for (int j = 0; j < mazeWidth; j++)
            {
                Vector3 spawnPos = new Vector3(j*10, 0, i*10);
                GameObject cell = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
                cell.transform.SetParent(cellContainer.transform);
                row[j] = cell;

                CellController cg = cell.GetComponent<CellController>();
                cg.row = i;
                cg.col = j;
            }

            cells[i] = row;
        }
    }

    private void generateRoute(GameObject cell)
    {
        CellController cg = cell.GetComponent<CellController>();
        cg.VisitCell();
        GameObject[] unvisitedNeighbors = cg.GetUnvisitedNeighbors(cells);
        while (unvisitedNeighbors.Length > 0)
        {
            int rand = Random.Range(0, unvisitedNeighbors.Length);
            GameObject nextCell = unvisitedNeighbors[rand];
            removeWalls(cell.GetComponent<CellController>(), nextCell.GetComponent<CellController>());
            generateRoute(nextCell);
            unvisitedNeighbors = cg.GetUnvisitedNeighbors(cells);
        }
    }

    private void removeWalls(CellController cell, CellController nextCell)
    {
        if(cell.row != nextCell.row)
        {
            if(cell.row < nextCell.row)
            {
                cell.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                nextCell.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            } else
            {
                cell.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                nextCell.gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }
        } else
        {
            if (cell.col < nextCell.col)
            {
                cell.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                nextCell.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                cell.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                nextCell.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    private void ScaleFloor()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Ground");
        foreach(GameObject floor in floors)
        {
            floor.transform.localScale = new Vector3(mazeWidth * 10, floor.transform.localScale.y, mazeHeight * 10);
            floor.transform.position = new Vector3(mazeWidth * 5, floor.transform.position.y, mazeHeight * 5);
        }
    }

    private IEnumerator iterativeDepthFirstSearch(GameObject cell)
    {
        Stack<GameObject> cellStack = new Stack<GameObject>();
        cellStack.Push(cell);
        CellController cg = cell.GetComponent<CellController>();
        cg.VisitCell();
        while(cellStack.Count > 0)
        {
            GameObject current = cellStack.Pop();
            CellController currentCG = current.GetComponent<CellController>();
            GameObject[] unvisitedNeighbors = currentCG.GetUnvisitedNeighbors(cells);
            if (unvisitedNeighbors.Length > 0)
            {
                cellStack.Push(current);
                int rand = Random.Range(0, unvisitedNeighbors.Length);
                GameObject nextCell = unvisitedNeighbors[rand];
                CellController nextCG = nextCell.GetComponent<CellController>();
                removeWalls(currentCG, nextCG);
                nextCG.VisitCell();
                cellStack.Push(nextCell);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
