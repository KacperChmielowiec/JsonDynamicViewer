using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Avalonia3.Models
{
    internal class JValueTree :  ItreeToken
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public object Value { get; set; }
        public ItreeToken.JTokenType Type { get; set; }
        public ValueType valueType { get; set; }
        public Guid File { get; set; }
        public JContainerTree Parent { get; set; }

        public enum ValueType
        {
            Boolean, Byte, Char, Decimal, Double, Int, String, Null, Float
        }

        public JValueTree(object value)
        {

            this.Type = ItreeToken.JTokenType.Value;
            Type targetType = value.GetType();

            try
            {

                object converted = Convert.ChangeType(value, targetType);
                this.Value = converted;

                switch (targetType.Name)
                {
                    case "Float":
                        valueType = ValueType.Float;
                        break;
                    case "Boolean":
                        valueType = ValueType.Boolean;
                        break;
                    case "Byte":
                        valueType = ValueType.Byte;
                        break;
                    case "Char":
                        valueType = ValueType.Char;
                        break;
                    case "Decimal":
                        valueType = ValueType.Decimal;
                        break;
                    case "Double":
                        valueType = ValueType.Double;
                        break;
                    case "Int16":
                        valueType = ValueType.Int;
                        break;
                    case "Int32":
                        valueType = ValueType.Int;
                        break;
                    case "Int64":
                        valueType = ValueType.Int;
                        break;
                    case "String":
                        valueType = ValueType.String;
                        break;
                    default:
                        valueType = ValueType.Null;
                        break;

                }


            }
            catch (Exception e)
            {
                // Ignorujemy błąd i przechodzimy do następnego typu
            }

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
        public ItreeToken GetValue()
        {
            return this;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        public string ToString(int c)
        {
            return Value.ToString();
        }

    }
}
