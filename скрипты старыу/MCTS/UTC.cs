using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MCTS
{
    class UTC
    {
       
            public static float uctValue( int totalVisit, double nodeWinScore, int nodeVisit)
            {
                if (nodeVisit == 0)
                {
                    return int.MaxValue;
                }
                return ((float)nodeWinScore / (float)nodeVisit) + 1.41f *Mathf.Sqrt(Mathf.Log(totalVisit) / (float)nodeVisit);
            }

            public static Node findBestNodeWithUCT(Node node)
            {

            List<Node> children = node.GetChildren();
            State s = node.GetState();
            int parentVisit =s.VisitCount;

            return getMaxNode( parentVisit,  children);
            }


        public static Node getMaxNode(int parentVisit, List<Node> children)
        {
            if (children.Count == 0)
                return null;

            Node n = children[0];
            float max = uctValue(parentVisit, n.GetState().WinScore, n.GetState().VisitCount);

           
            for (int i = 1; i < children.Count; i++)
            {
                float score = uctValue(parentVisit, children[i].GetState().WinScore, children[i].GetState().VisitCount);

                if (max < score)
                {

                    max = score;
                    n = children[i];
                }

            }
           // Debug.Log(max);
            return n;
        }
        
    }
}
