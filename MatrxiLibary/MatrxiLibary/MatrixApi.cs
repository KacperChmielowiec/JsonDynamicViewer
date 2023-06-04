using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrxiLibary
{
    public interface Idata
    {
           public double[] matrix { get; set; }
            

    }

    public class data : Idata
    {
        public double[] matrix { set; get; }
        public data(int[] m)
        {
            this.matrix = Array.ConvertAll<int, double>(m, m => m);
        }
        public data(double[] m)
        {
            this.matrix = m;
        }
        public data(float[] m)
        {
            this.matrix = Array.ConvertAll<float, double>(m, m => m);
        }
    }


    public class MatrixApi<T> where T : Idata
    {
        public T[] matrix { get; set; }
       
        public MatrixApi(T data)
        {

            this.matrix[0] = data;

        }
        public MatrixApi(T[] data)
        {

            this.matrix = data;

        }



    }
}

