using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell
    {
        public bool visited=false;
        public bool[] status = new bool[4];
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
            //0 - cannot spawn 1- can spawn 2- has to spawn

            if(x>=minPosition.x && x<=maxPosition.x && y>=minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }
            return 0;
        }
    }
    public Vector2Int size;
    public int startPos=0;
    public Rule[] rooms;
    //public GameObject room;
    public Vector2 offset;
    public int lastVisitedCellIndex = -1;
    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[i + j * size.x];
                if (currentCell.visited)
                {
                    int roomIndex;
                    if (i + j * size.x == lastVisitedCellIndex)
                    {
                        // ������ �湮�� ���� ��ġ�� room3 ��ü�� �����մϴ�.
                        // room3�� rooms �迭���� �� ��° �ε����� ��ġ�ϴ��� �˾ƾ� �մϴ�.
                        // ���� ���, room3�� rooms �迭�� �� ��° ��Ҷ�� �����մϴ�.
                        roomIndex = 2; // rooms �迭���� room3�� �ε���
                    }
                    else
                    {
                        // ���� �������� ���� �����մϴ�.
                        List<int> availableRooms = new List<int>();
                        for (int k = 0; k < rooms.Length; k++)
                        {
                            int probability = rooms[k].ProbabilityOfSpawning(i, j);
                            if (probability == 2) // �ʼ� ��
                            {
                                roomIndex = k;
                                break;
                            }
                            else if (probability == 1) // ���� ������ ��
                            {
                                availableRooms.Add(k);
                            }
                        }

                        if (availableRooms.Count > 0)
                        {
                            roomIndex = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            // ������ �� �ִ� ���� ���� ��� �⺻ ���� ����
                            roomIndex = 0; // �⺻ ���� �ε���
                        }
                    }

                    var newRoom = Instantiate(rooms[roomIndex].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        for(int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();

        int k=0;

        while(k<36)
        {
            k++;
            
            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            //check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);
            if (neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell=path.Pop();
                }

            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if(newCell>currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell=newCell;
                        board[currentCell].status[1] = true;
                    }
                  
                }
            }
        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        int x = cell % (int)size.x; 
        int y = cell / (int)size.x;

        //��
        if (y > 0 && !board[cell - (int)size.x].visited)
        {
            neighbors.Add(cell - (int)size.x);
        }
        //�Ʒ�
        if (y < size.y - 1 && !board[cell + (int)size.x].visited)
        {
            neighbors.Add(cell + (int)size.x);
        }
        //��
        if (x < size.x - 1 && !board[cell + 1].visited)
        {
            neighbors.Add(cell + 1);
        }
        //��
        if (x > 0 && !board[cell - 1].visited)
        {
            neighbors.Add(cell - 1);
        }

        return neighbors;

    }

}
