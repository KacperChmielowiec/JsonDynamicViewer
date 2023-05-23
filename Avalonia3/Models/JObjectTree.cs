using Avalonia.FreeDesktop.DBusIme;
using Avalonia3.Interface;
using DynamicData;
using Fizzler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public class JObjectTree : JContainerTree, INotifyPropertyChanged
    {

        

        public JObjectTree()
        {
            this.ChildrenCollection = new ObservableCollection<ItreeToken>();
            this.Type = ItreeToken.JTokenType.Object;
        }
        public override ItreeToken GetValue()
        {
            return this;
        }
        public override int GetHashCode()
        {
            return this.ChildrenCollection.GetHashCode();
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
            if (token is JPropertyTree property)
            {
                ChildrenCollection.Add(property);
            }
            else
                throw new ArgumentException($"Bad Type for {this.GetType()}. Element is {token.GetType()} Type (Method Add)");
        }
        public void Add(string name,ItreeToken token)
        {
            if (token is JPropertyTree property) throw new ArgumentException($"Bad Type for {this.GetType()} Method's Add(string,token). Element is {token.GetType()} Type");
            ItreeToken item;
            item = token.GetValue();
            JPropertyTree prop = new JPropertyTree(name, item);
            prop.ParentId = item.ParentId;
            prop.Id = item.Id;
            ChildrenCollection.Add(prop);
        }

        public override ObservableCollection<ItreeToken> Children()
        {
            return this.ChildrenCollection.OfType<ItreeToken>() as ObservableCollection<ItreeToken>;
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
            foreach (var item in this.ChildrenCollection)
            {
                if (item is JPropertyTree property)
                {
                    stringBuilder.AppendLine(property.Name + ": " + property.Value.ToString(c + 2));
                    stringBuilder.Append(new string(' ', c + 2));
                }
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

                return this.ChildrenCollection.Remove(item);

            }
            return false;
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
            if (item is JPropertyTree property)
            {
                int index = this.ChildrenCollection
                    .Select((x, y) => (x,y))
                    .Where(x => x.x.Id == property.Id)
                    .Select(x => x.y).FirstOrDefault();
               
                this.ChildrenCollection.RemoveAt(index);
                return true;
            }
            throw new ArgumentException($"Bad Type for {this.GetType()}. Element is {item.GetType()} Type (Method Remove)");

        }

        public override IEnumerator<ItreeToken> GetEnumerator()
        {
            yield return this;
        }
        public JPropertyTree this[string i]
        {
            get
            {
                return this.ChildrenCollection.OfType<JPropertyTree>().ToList().FirstOrDefault(x => x.Name == i);
            }
            
        }
    }
}
