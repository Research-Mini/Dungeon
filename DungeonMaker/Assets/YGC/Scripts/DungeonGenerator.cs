using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YGC
{
    public class DungeonGenerator : MonoBehaviour
    {
        public class Cell
        {
            public bool visited = false;
            public bool[] status = new bool[4]; // Up down Right left
        }

        [System.Serializable]
        public class Rule
        {
            public GameObject room;
            public Vector2Int minPosition;
            public Vector2Int maxPosition;

            public bool obligatory;

            public int ProbabilityOfSpawning(int x, int y)
            {
                // 0 - cannot spawn at that position
                // 1 - can spawn at that position
                // 2 - Has to spawn at that position

                if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
                {
                    return obligatory ? 2 : 1;
                }

                return 0;
            }
        }

        public Vector2Int size;
        public int startPos = 0;
        List<Cell> board;

        public Rule[] rooms;
        public Vector2 offset;

        public GameObject enemyObj;
        public int totalEnemy = 0;
        private List<GameObject> total_Rooms = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            MazeGenerator();
            GenerateDungeon();
            GenerateEnemy();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void GenerateEnemy()
        {
            List<int> enemyPosList = new List<int>();

            while(enemyPosList.Count != totalEnemy)
            {
                int enemyPos = Random.Range(0, total_Rooms.Count);

                if(!enemyPosList.Contains(enemyPos))
                {
                    enemyPosList.Add(enemyPos);
                    var newEnemy = Instantiate(enemyObj);
                    newEnemy.SetActive(true);
                    newEnemy.transform.SetParent(total_Rooms[enemyPos].transform);
                    newEnemy.transform.localPosition = Vector3.zero;
                }
            }
        }

        void GenerateDungeon()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Cell currentCell = board[(i + j * size.x)];
                    if (currentCell.visited)
                    {
                        int randomRoom = -1;

                        List<int> availableRooms = new List<int>();

                        for (int k = 0; k < rooms.Length; k++)
                        {
                            int p = rooms[k].ProbabilityOfSpawning(i, j);

                            if (p == 2)
                            {
                                randomRoom = k;
                                break;
                            }
                            else if (p == 1)
                            {
                                availableRooms.Add(k);
                            }
                        }

                        if (randomRoom == -1)
                        {
                            if (availableRooms.Count > 0)
                            {
                                randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                            }
                            else
                            {
                                randomRoom = 0;
                            }
                        }

                        var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(board[(i + j * size.x)].status);

                        newRoom.name += " " + i + "-" + j;

                        // 맨 첫칸은 NPC 있어서 적 안 생기게 함.
                        if(!rooms[randomRoom].obligatory)
                        total_Rooms.Add(newRoom.gameObject);
                    };
                }
            }
        }

        void MazeGenerator()
        {
            board = new List<Cell>();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    board.Add(new Cell());
                }
            }

            int currentCell = startPos;

            Stack<int> path = new Stack<int>();

            int k = 0;

            while (k < 1000) // 던전 크기 1000으로 제한.
            {
                k++;

                board[currentCell].visited = true;

                if (currentCell == board.Count - 1)
                {
                    break;
                }

                // Check the Cell's neighbors
                List<int> neighbors = CheckNeighbors(currentCell);

                if (neighbors.Count == 0)
                {
                    if (path.Count == 0) break;

                    currentCell = path.Pop();
                }
                else
                {
                    path.Push(currentCell);

                    int newCell = neighbors[Random.Range(0, neighbors.Count)];

                    // down or right
                    if (newCell > currentCell)
                    {
                        if (newCell - 1 == currentCell)
                        {
                            // right
                            board[currentCell].status[2] = true;
                            currentCell = newCell;

                            board[currentCell].status[3] = true;
                        }
                        else
                        {
                            // down
                            board[currentCell].status[1] = true;
                            currentCell = newCell;

                            board[currentCell].status[0] = true;
                        }
                    }
                    else // up or left
                    {
                        if (newCell + 1 == currentCell)
                        {
                            // right
                            board[currentCell].status[3] = true;
                            currentCell = newCell;

                            board[currentCell].status[2] = true;
                        }
                        else
                        {
                            // down
                            board[currentCell].status[0] = true;
                            currentCell = newCell;

                            board[currentCell].status[1] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param> 현재 체크 중인 Cell.
        /// <returns></returns>
        List<int> CheckNeighbors(int cell)
        {
            List<int> neighbors = new List<int>();

            // Check up neighbor
            if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
            {
                neighbors.Add((cell - size.x));
            }

            // Check down neighbor
            if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
            {
                neighbors.Add((cell + size.x));
            }

            // Check right neighbor
            if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
            {
                neighbors.Add((cell + 1));
            }

            // Check left neighbor
            if (cell % size.x != 0 && !board[(cell - 1)].visited)
            {
                neighbors.Add((cell - 1));
            }

            return neighbors;
        }
    }

}
