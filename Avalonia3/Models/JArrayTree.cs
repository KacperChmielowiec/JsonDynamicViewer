using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.Interface;

namespace Avalonia3.Models
{
    internal class JArrayTree : JContainerTree
    {
        public JArrayTree() {

            
            this.Type = ItreeToken.JTokenType.Array;
            this.ChildrenCollection = new ObservableCollection<ItreeToken>();
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as ItreeToken);
        }
        public bool Equals(ItreeToken obj)
        {
            return false;
        }
        public override ItreeToken GetValue()
        {
            return this;
        }

        public override void Add(ItreeToken token)
        {
            ChildrenCollection.Add(token.GetValue());
        }

        public override ObservableCollection<ItreeToken> Children()
        {
           return this.ChildrenCollection;
        }
        public override string ToString()
        {
            return this.ToString(0);
        }
        public override string ToString(int c = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");
        
            stringBuilder.Append(new string(' ', c+2));
            foreach (var item in ChildrenCollection)
            {
                stringBuilder.AppendLine(item.ToString(c+2));
                stringBuilder.Append(new string(' ', c+2));

            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
         
            stringBuilder.AppendLine("]");
            return stringBuilder.ToString().TrimEnd(); 
        }

        public override int Count()
        {
            throw new NotImplementedException();
        }

        public override bool IsReadOnly()
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(ItreeToken item)
        {
            throw new NotImplementedException();
        }

        public override void CopyTo(ItreeToken[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ItreeToken item)
        {
            int index = this.ChildrenCollection
                    .Select((x, y) => (x, y))
                    .Where(x => x.x.Id == item.Id)
                    .Select(x => x.y).FirstOrDefault();
            
            this.ChildrenCollection.RemoveAt(index);
            return true;
        }
        public override bool Remove(Guid item)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<ItreeToken> GetEnumerator()
        {
            yield return this;
        }
    }
}
