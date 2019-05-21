using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace TANnetwork
{
    struct Coordinate
    {
        public double longitude;
        public double latitude;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, double>> graph = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Coordinate> coordinates = new Dictionary<string, Coordinate>();
            Dictionary<string, string> Names = new Dictionary<string, string>();
            CultureInfo culture = CultureInfo.InvariantCulture;
            string startPoint = "";
            string endPoint = "";
            using (StreamReader sr = new StreamReader("../../testdata/data2.txt", Encoding.Default))
            {
                startPoint = sr.ReadLine();
                endPoint = sr.ReadLine();
                int N = int.Parse(sr.ReadLine());
                for (int i = 0; i < N; i++)
                {
                    string stopName = sr.ReadLine();

                }
            }
           
            //string startPoint = Console.ReadLine();
            //startPoint = startPoint.Split(':')[1];
            //string endPoint = Console.ReadLine();
            //endPoint = endPoint.Split(':')[1];
            //int N = int.Parse(Console.ReadLine());
            //for (int i = 0; i < N; i++)
            //{
            //    string stopName = Console.ReadLine();
            //    string[] information = stopName.Split(',');

            //    string id = information[0].Split(':')[1];
            //    string name = information[1];
            //    Names.Add(id, name);

            //    string latitude = information[3];
            //    string longitude = information[4];
            //    coordinates.Add(id, new Coordinate()
            //    {
            //        latitude = double.Parse(latitude, culture.NumberFormat),
            //        longitude = double.Parse(longitude, culture.NumberFormat)
            //    });
            //}
            

            using (StreamReader sr = new StreamReader("../../testdata/data.txt", Encoding.Default))
            {
                int M = int.Parse(sr.ReadLine());
                for (int i = 0; i < M; i++)
                {
                    string route = sr.ReadLine();
                    string station1 = route.Split(';')[0];
                    string station2 = route.Split(';')[1];
                    double dist = double.Parse(route.Split(';')[2]);
                    //Coordinate coorA = coordinates[station1];
                    //Coordinate coorB = coordinates[station2];
                    //double x = (coorB.longitude - coorA.longitude) * Math.Cos((coorA.latitude + coorB.latitude) / 2);
                    //double y = coorB.latitude - coorA.latitude;
                    //double dist = Math.Sqrt(x * x + y * y) * 6371;

                    if (!graph.Keys.Contains(station1))
                    {
                        graph.Add(station1, new Dictionary<string, double>());
                        graph[station1][station2] = dist;
                    }
                    else
                    {
                        graph[station1].Add(station2, dist);

                    }
                    if (!graph.Keys.Contains(station2))
                    {
                        graph.Add(station2, new Dictionary<string, double>() { [station1] = dist });
                    }
                    else
                    {
                        graph[station2].Add(station1, dist);
                    }
                }
            }
            

            List<string> Path = PathFinder.Solve(graph, startPoint, endPoint);
            Path.Reverse();
            foreach(var p in Path)
            {
                Console.WriteLine(Names[p].Replace("\"", ""));
            }

            return;
        }
    }
}
