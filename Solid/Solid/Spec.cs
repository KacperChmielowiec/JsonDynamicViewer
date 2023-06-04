using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid
{

    public class Product
    {
        public int size { get; set; }
        public string color { get; set; }

        public Product(int s, string c)
        {
            this.size = s;
            this.color = c;

        }
    }
    public interface ISpecification<T>
    { 
        bool IsSatisfied(T item);
    }
    public interface IFilter<T>
    { 
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> Spec );
    }

    public class Filter1: IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(Product p in items)
            {
                if (spec.IsSatisfied(p)) yield return p;
            }
        }

    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first,
        ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

}
