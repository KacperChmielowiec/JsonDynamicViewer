using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    internal class JObjectTree : JContainerTree, INotifyPropertyChanged
    {
        private Dictionary<string,JPropertyTree> _children;

        public JObjectTree()
        {
            this._children = new Dictionary<string, JPropertyTree>();
            this.ChildrenCollection = new ObservableCollection<ItreeToken> { };
            this.Type = ItreeToken.JTokenType.Object;

        }
        public override ItreeToken GetValue()
        {
            return this;
        }
        public override int GetHashCode()
        {
            return this._children.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as JObjectTree);
        }
        public bool Equals(JObjectTree obj)
        {
            return false;
        }
        public override void Add(ItreeToken token)
        {
            if(!(token is JPropertyTree))
            {
                throw new ArgumentException();
            }
            _children.TryAdd((token as JPropertyTree).Name, (token as JPropertyTree));
            ChildrenCollection.Add(token);
        }
        public void Add(string name,ItreeToken token)
        {
            ItreeToken item;
            item = token.GetValue();
            JPropertyTree prop = new JPropertyTree(name, item);
            prop.ParentId = item.ParentId;
            prop.Id = item.Id;
            _children.TryAdd(name,prop);
            ChildrenCollection.Add(prop);
        }

        public override ObservableCollection<ItreeToken> Children()
        {
            this.ChildrenCollection = new ObservableCollection<ItreeToken>(_children.Values);
            return this.ChildrenCollection;
        } 

        public JPropertyTree this[string i]
        {
            get
            {
                if (_children.ContainsKey(i))
                {
                    return _children[i];
                }
                else
                {
                    return null;
                }

            }
            set { _children[i] = value; }
        }


        public override string ToString()
        {
            return this.ToString(0);
        }
        public override string ToString(int c)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("{");
           
            stringBuilder.Append(new string(' ', c + 2));
            foreach (var item in _children)
            {
                stringBuilder.AppendLine(item.Key + ": " + item.Value.ToString(c+2));
                stringBuilder.Append(new string(' ', c+2));
            }
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
        
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString().TrimEnd();
        }


        public override bool Remove(Guid id)
        {

            var item = this.ChildrenCollection.Where(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {

                string name = (item as JPropertyTree).Name;
                _children.Remove(name);
            }
            return this.ChildrenCollection.Remove(item);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            this._children.Clear();
            this.ChildrenCollection.Clear();
           
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
            string name = (item as JPropertyTree).Name;
            _children.Remove(name);
            return ChildrenCollection.Remove(item);

        }

        public override IEnumerator<ItreeToken> GetEnumerator()
        {
            yield return this;
        }
    }
}
