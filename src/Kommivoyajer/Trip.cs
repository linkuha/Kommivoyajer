using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Kommivoyajer
{
    //Наверное переделать
    class Trip
    {
        public List<int> cities;
        public int total;
        public string citiesString;

        public Trip(List<int> c, int l)
        {
            cities = new List<int>(c);
            total = l;
        }


        public String toString()
        {

            foreach (var distances in cities)
            {
                citiesString += distances + " -> ";
            }

            return "Trip distances: " + citiesString + cities[0] + ", shortestTrip: " + total + "]";

        }

    }
}
