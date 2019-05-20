using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TANnetwork
{
    class PathFinder
    {
        static List<string> Processed = new List<string>();
        public static List<string> Solve(Dictionary<string, Dictionary<string, double>> graph, string startpoint, string endpoint)
        {
          
            Dictionary<string, double> Costs = new Dictionary<string, double>();
            foreach(var dist in graph[startpoint])
            {
                Costs.Add(dist.Key, dist.Value);
            }
            if (!Costs.Keys.Contains(endpoint))
            {
                Costs.Add(endpoint, double.PositiveInfinity);
            }
           
            Dictionary<string, string> Parents = new Dictionary<string, string>();
            foreach(var dist in graph[startpoint])
            {
                Parents.Add(dist.Key, startpoint);
            }
            if (!Parents.Keys.Contains(endpoint))
            {
                Parents.Add(endpoint, null);
            }


            string node = FindLowestCostNode(Costs);
            while (node != null)
            {
                double cost = Costs[node];
                var neighbors = graph[node];
                foreach (var n in neighbors)
                {
                    double newcost = cost + n.Value;
                    if (Costs.Keys.Contains(n.Key) && Costs[n.Key] > newcost)
                    {
                        Costs[n.Key] = newcost;
                        Parents[n.Key] = node;
                    }

                }
                Processed.Add(node);
                node = FindLowestCostNode(Costs);
            }
            List<string> result = new List<string>();
            string CurrentNode = endpoint;
            while (CurrentNode != startpoint)
            {

                result.Add(CurrentNode);
                CurrentNode = Parents[CurrentNode];
            }
            result.Add(startpoint);
            return result;
        }

        private static string FindLowestCostNode(Dictionary<string, double> Costs)
        {
            double lowestCost = double.PositiveInfinity;
            string lowestCostNode = null;
            foreach (var node in Costs)
            {
                double cost = node.Value;
                if (cost < lowestCost && !Processed.Contains(node.Key))
                {
                    lowestCost = cost;
                    lowestCostNode = node.Key;
                }
            }
            return lowestCostNode;

        }
    }
}
