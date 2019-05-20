using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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

            CultureInfo culture = CultureInfo.InvariantCulture;

            string startPoint = Console.ReadLine();
            string endPoint = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                string stopName = Console.ReadLine();
                string[] information = stopName.Split(',');
                string id = information[0].Split(':')[1];
                string latitude = information[3];
                string longitude = information[4];
                coordinates.Add(id, new Coordinate()
                {
                    latitude = double.Parse(latitude, culture.NumberFormat),
                    longitude = double.Parse(longitude, culture.NumberFormat)
                });

            }
            int M = int.Parse(Console.ReadLine());
            for (int i = 0; i < M; i++)
            {
                string route = Console.ReadLine();
                string station1 = route.Split(' ')[0].Split(':')[1];
                string station2 = route.Split(' ')[1].Split(':')[1];

                Coordinate coorA = coordinates[station1];
                Coordinate coorB = coordinates[station2];
                double x = (coorB.longitude - coorA.longitude) * Math.Cos((coorA.latitude + coorB.latitude) / 2);
                double y = coorB.latitude - coorA.latitude;
                double dist = Math.Sqrt(x * x + y * y) * 6371;

                if (!graph.Keys.Contains(station1))
                {
                    graph.Add(station1, new Dictionary<string, double>() { [station2] = dist });
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
    }
}
