using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Kommivoyajer.Methods
{
    class ExhaustiveSearch
    {
        public int Total {get; set;}
        static int[,] tempMatrix;
        static int cities_count = 0;
        int maxPath = int.MaxValue;
        public List<int> CitiesOrder { get; set; }

        ArrayList tripList = new ArrayList();

        private List<int> singleCity = new List<int>();
        private int _iteration;
        public int Iteration
        {
            get { return _iteration; }
            set { _iteration = value; }
        }

        public ExhaustiveSearch(int cities, double[,] distanceMatrix)
        {
            cities_count = cities;
            tempMatrix = diArray(distanceMatrix);
        }

        private int[,] diArray(double[,] val)
        {
            var ret = new int[val.GetLength(0), val.GetLength(1)];
            for (int i = 0; i < val.GetLength(0); i++)
            {
                for (int j = 0; j < val.GetLength(1); j++)
                {
                    ret[i, j] = (int)val[i, j];
                }
            }
            return ret;
        }

        public Boolean distanceReader(List<int> cities)
        {
            int sum = 0;
            int row = 0;
            int returnValue = 0;
            int column = 1;

            for (int i = 0; i < cities_count - 1; i++)
            {
                int value = Convert.ToInt32(tempMatrix[cities.ElementAt(column), cities.ElementAt(row)]);
                if (value == 0)
                {
                    value = Convert.ToInt32(tempMatrix[cities.ElementAt(column), cities.ElementAt(row)]); ;
                }
                sum += value;
                row++;
                column++;
            }
            returnValue = Convert.ToInt32(tempMatrix[cities.ElementAt(column - 1), cities.ElementAt(0)]);   //+ обратный путь
            int total = sum + returnValue;

            if (tripList.Count == 0) tripList.Add(new Trip(cities, total));

            if (total < ((Trip)tripList[0]).total)
            {
                tripList.Clear();
                tripList.Add(new Trip(cities, total));
                CitiesOrder = cities;
            }
            _iteration++;
            if (sum <= maxPath)
            {
                return false;
            }
            return true;
       
        }
        /**
    * Найти все возможные пути
    */
        private Boolean permutations(List<int> used, List<int> unused)
        {

            if (!unused.Any())
            {
                if (used[0] != 0)
                {
                    unused = singleCity; //заканчиваем перебор, когда переберем все варианты для входной точки
                    return true;
                }
                if (distanceReader(used))
                {
                    return true;
                }
            }
            else
            {

                for (int i = 0; i < unused.Count(); i++)
                {
                    int item = unused.ElementAt(0);
                    unused.RemoveAt(0);
                    used.Add(item);

                    if (permutations(used, unused))
                        return true;
                    used.RemoveAt(used.Count() - 1);
                    unused.Add(item);
                }
            }

            return false;

        }


        public Boolean generateCities()
        {
            List<int> cityList = new List<int>();
            for (int i = 0; i < cities_count; i++)
            {
                cityList.Add(i);
                singleCity.Add(i);
            }

            return permutations(new List<int>(), cityList);
        }


        public String printShortTrip()
        {

            //int min = maxPath;
            Trip minTrip = null;

            //for (int i = 0; i < tripList.Count; i++)
            //{
            //    if (((Trip)tripList[i]).total < min)
            //    {
            //        min = ((Trip)tripList[i]).total;
            //        minTrip = (Trip)tripList[i];
            //    }
            //}
            minTrip = (Trip)tripList[0];
            CitiesOrder = minTrip.cities;
            Total  = ((Trip)tripList[0]).total;
            String trip = minTrip.toString();
            return trip;

        }
    }
}

