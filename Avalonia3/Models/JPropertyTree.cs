﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia3.Interface;

namespace Avalonia3.Models
{
    public class JPropertyTree : ItreeToken
    {
        
        public JContainerTree Parent { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public ItreeToken.JTokenType Type { get; set; }
        public ItreeToken Value { get; set; }
        public string Name { get; set; }

        public JPropertyTree(string name, ItreeToken token)
        {

            if (token == null) throw new ArgumentNullException();
            Value = token.GetValue();
            Name = name;

            this.Type = ItreeToken.JTokenType.Property;

        }
        public JPropertyTree(string name, object token)
        {

            if (token == null) throw new ArgumentNullException();
            else if (token.GetType().IsValueType || token is string)
            {
                this.Value = new JValueTree(token);
                this.Name = name;
                Type = ItreeToken.JTokenType.Value;
            }
            else
            {
                throw new ArgumentException();
            }

        }
        public ItreeToken GetValue()
        {
            return this.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as JPropertyTree);
        }
        public bool Equals(JPropertyTree obj)
        {
            return true;
        }
        public void CheckType()
        {

            switch (this.Value.GetType().Name)
            {
                case "JObjectTree":
                    Type = ItreeToken.JTokenType.Object; break;
                case "JArrayTree":
                    Type = ItreeToken.JTokenType.Array; break;
                default: 
                    Type = ItreeToken.JTokenType.Value; break;
            }

        }
        public override string ToString()
        {
            return this.Name + ": " + this.Value.ToString();
        }
        public string ToString(int c)
        {
            return Value.ToString(c + 2);
        }
       
      
    }
}
