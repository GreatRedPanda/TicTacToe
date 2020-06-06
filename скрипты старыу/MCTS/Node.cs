using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace Assets.Scripts.MCTS
{
    class Node
    {
        State state=new State();
       public  Node parent;
        List<Node> childArray=new List<Node>();

        public int NodeNumber;

        public static int AllCreatedNodes = 0;
        public Node()
        {
            NodeNumber = AllCreatedNodes;
            AllCreatedNodes++;
        }
        public Node(State s)
        {
            NodeNumber = AllCreatedNodes;
            AllCreatedNodes++;
            state = s;  
          }
        public State GetState()
        {

            return state;
        }

        public List<Node> GetChildren()
        {

            return childArray;
        }


        public void AddChild(Node n)
        {

            childArray.Add(n);
        }

     public Node  getRandomChildNode()
        {

            int randIndex = UnityEngine.Random.Range(0, childArray.Count);


            return childArray[randIndex];

        }

        public Node getChildWithMaxScore()
        {
            Debug.Log(childArray.Count + "rooot node children");
            if (childArray.Count == 0)
                return null;

            Node n = childArray[0];
            float max = n.state.WinScore;


            for (int i = 1; i < childArray.Count; i++)
            {
                float score = childArray[i].state.WinScore;

                if (max < score)
                {

                    max = score;
                    n = childArray[i];
                }

            }
            return n;

        }

        public Node(Node source)
        {


        
        }


       
    }
}
