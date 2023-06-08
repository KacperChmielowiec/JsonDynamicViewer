using Avalonia.Controls;
using Avalonia3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.References;
using Avalonia3.Interface;
using Avalonia3.Models;
using Avalonia3.ViewModels.UserControls;
using ExCSS;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.Json;
using static Avalonia3.Models.JValueTree;

namespace Avalonia3.Services
{
    internal class AdvanceJsonService
    {
        MainModelView MainModel;
    
        public AdvanceJsonService() {

            MainModel = MainModelView.Instance;
         
        }
        public void LoadDataToChart(Guid _guid, ValueNodeViewModel ValueModel)
        {
            var File = JsonMap.files.Where(x => x.IdJson == _guid).FirstOrDefault();
            if (File != null)
            {
                var JsonContext = File.KeyValueMap[File.IdRoot];
                var JsonElement = JsonContext.TreeToken;
                ValueNodeViewModel ValueNodeViewModel = ConstructViewModel(JsonElement,0,JsonElement.Type);
                MainModel.ValueNode = ValueNodeViewModel;
            }
        }
        private ValueNodeViewModel ConstructViewModel(ItreeToken token,short nesting, ItreeToken.JTokenType TypeToken, string? PropName = null)
        {

            if (token == null)
            {
                return new ValueNodeViewModel(nesting, PropName);
            }
            if (TypeToken == ItreeToken.JTokenType.Array)
            {
                return ConstructViewModelFromArray(token as JArrayTree, nesting, PropName);
            }
            if (TypeToken == ItreeToken.JTokenType.Object)
            {
                return ConstructViewModelFromObject(token as JObjectTree, nesting, PropName);
            }
            if(TypeToken == ItreeToken.JTokenType.Property)
            {
                return ConstructViewModel((token as JPropertyTree).Value,nesting,(token as JPropertyTree).Type,(token as JPropertyTree).Name);
            }

            var jsonElement = token.Type;
            switch (jsonElement)
            {
                case ItreeToken.JTokenType.Value:
                    return ConstructsViewModelFromValue(token as JValueTree, nesting, PropName);
                default:
                    throw new ArgumentException("Node value type not recognized");
            }
        }

        private ValueNodeViewModel ConstructViewModelFromObject(JObjectTree token, short nesting, string? propName)
        {
            var propNesting = (short)(nesting + 1);
            var Elements = token.ChildrenCollection.Select(token =>
            {
                var PropTemp = token as JPropertyTree;
                return new { Value = PropTemp.Value, Name = PropTemp.Name };

            });
            var properties = Elements.Select(element => ConstructViewModel(element.Value, propNesting, element.Value.Type, element.Name)).ToList();
            return new ValueNodeViewModel(new ObjectNodeViewModel(properties, nesting, propName), nesting);
        }

        private ValueNodeViewModel ConstructViewModelFromArray(JArrayTree token, short nesting, string? propName)
        {
            var itemNesting = (short)(nesting + 1);
            var items = token.ChildrenCollection.Select(x => ConstructViewModel(x, itemNesting,x.Type)).ToList();
            return new ValueNodeViewModel(new ArrayNodeViewModel(items, nesting, propName), nesting);
        }
        private ValueNodeViewModel ConstructsViewModelFromValue(JValueTree token, short nesting, string? propName)
        {

            switch (token.valueType)
            {
                case JValueTree.ValueType.Null:
                    return new ValueNodeViewModel(nesting, propName);
                case JValueTree.ValueType.String:
                    return new ValueNodeViewModel(token.Value.ToString(),nesting,propName);
                case JValueTree.ValueType.Int:
                    return new ValueNodeViewModel(Convert.ToDouble(token.Value), nesting, propName);
                case JValueTree.ValueType.Boolean:
                    if(Convert.ToBoolean(token.Value) == true)
                        return new ValueNodeViewModel(true, nesting, propName);
                    return new ValueNodeViewModel(false, nesting, propName);
                case JValueTree.ValueType.Double:
                    return new ValueNodeViewModel(Convert.ToDouble(token.Value), nesting, propName);
                case JValueTree.ValueType.Float:
                    return new ValueNodeViewModel(Convert.ToDouble(token.Value), nesting, propName);
                
                default:
                    throw new ArgumentException("Node value type not recognized");
            }
        }
    }
}
