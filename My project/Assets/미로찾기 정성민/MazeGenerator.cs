using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public Transform player; // 플레이어 Transform
    public Transform goal; // 목표 Transform

    private int[,] maze1;
    private int[,] maze2;
    private int currentMazeIndex = 1;

    void Start()
    {
        maze1 = GenerateMaze();
        maze2 = GenerateMaze();
        LoadMaze(currentMazeIndex);
    }

    int[,] GenerateMaze()
    {
        int[,] maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = Random.Range(0, 2); // 0: 길, 1: 벽
            }
        }
        return maze;
    }

    void LoadMaze(int mazeIndex)
    {
        int[,] maze = (mazeIndex == 1) ? maze1 : maze2;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                if (maze[x, y] == 1)
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(floorPrefab, position, Quaternion.identity);
                }
            }
        }
        SetPlayerAndGoalPositions(mazeIndex);
    }

    void SetPlayerAndGoalPositions(int mazeIndex)
    {
        if (mazeIndex == 1)
        {
            player.position = new Vector3(1, 0, 1); // Maze 1의 시작 지점
            goal.position = new Vector3(width - 2, 0, height - 2); // Maze 1의 목표 지점
        }
        else
        {
            player.position = new Vector3(1, 0, 1); // Maze 2의 시작 지점
            goal.position = new Vector3(width - 2, 0, height - 2); // Maze 2의 목표 지점
        }
    }
    public void ClearMaze()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(o);
        }
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Floor"))
        {
            Destroy(o);
        }
    }

    public void GoToNextMaze()
    {
        ClearMaze();
        currentMazeIndex = (currentMazeIndex == 1) ? 2 : 1;
        LoadMaze(currentMazeIndex);
    }
}
