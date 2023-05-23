
using Avalonia3.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public abstract class JContainerTree : ItreeToken , ICollection<ItreeToken>
    {
        public JContainerTree() {

        }
        public ObservableCollection<ItreeToken> ChildrenCollection { get; set; }
        public abstract IList<ItreeToken> Children();
        public ItreeToken.JTokenType Type { get; set; }
        public JContainerTree Parent { get; set; }
        public  Guid Id { get; set; }
        public  Guid ParentId { get; set; }

        int ICollection<ItreeToken>.Count => throw new NotImplementedException();

        bool ICollection<ItreeToken>.IsReadOnly => throw new NotImplementedException();

        public abstract ItreeToken GetValue();
        public abstract int Count();

        public abstract bool IsReadOnly();

        public abstract string ToString(int c);

        public abstract void Clear();

        public abstract bool Contains(ItreeToken item);

        public abstract void CopyTo(ItreeToken[] array, int arrayIndex);

        public abstract bool Remove(ItreeToken item);

        public abstract bool Remove(Guid id);

        public abstract void Add(ItreeToken token);
        public abstract IEnumerator<ItreeToken> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
