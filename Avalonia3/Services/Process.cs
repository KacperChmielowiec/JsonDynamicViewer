using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.Models;
using Microsoft.Win32.SafeHandles;

namespace Avalonia3.Services
{
    internal class Process
    {
        public static ItreeToken ProcessJsonData(JsonTextReader reader, Dictionary<Guid, ObjectContext> keyValuePairs, string State = "")
        {
            JObjectTree newObject = new JObjectTree();
            JArrayTree newArray = new JArrayTree();
            List<ItreeToken> recPropList = new List<ItreeToken>();
            var guid = Guid.NewGuid();
            JsonToken typeStart = JsonToken.None;
            JsonToken typeEnd = JsonToken.None;
            newObject.Id = guid;
            newArray.Id = guid;


            var lastProp = String.Empty;


            while (reader.Read())
            {

                if (reader.Value != null)
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.PropertyName:

                            if (newObject[reader.Value.ToString()] == null)
                            {
                                lastProp = reader.Value.ToString();
                            }
                            break;

                        default:
                            
                            Guid _g = Guid.NewGuid();


                            if(State == nameof(JArrayTree))
                            {
                                JValueTree val = new JValueTree(reader.Value);
                                val.Id = _g;
                                val.ParentId = guid;

                                val.Parent = newArray;
                                recPropList.Add(val);
                                
                                newArray.Add(val);

                            }
                            else
                            {
                                if (newObject[lastProp] == null && lastProp != "")
                                {
                                    JValueTree valueTree = new JValueTree(reader.Value);
                                    valueTree.Id = _g; 
                                    JPropertyTree val = new JPropertyTree(lastProp, valueTree);
                                    val.Id = _g;
                                    val.ParentId = guid;
                                    val.Parent = newObject;
                                    newObject.Add(val);
                                    recPropList.Add(val);

                                }
                            }
                            break;


                    }

                
                }
                else
                {

                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        if (lastProp != "")
                        {
                            State = nameof(JObjectTree);
                            typeStart = reader.TokenType;
                            var _item = ProcessJsonData(reader, keyValuePairs, State);
                            _item.Parent = newObject;
                            _item.ParentId = guid;
                            JPropertyTree prop = new JPropertyTree(lastProp, _item);
                            
                            prop.Parent = newObject;
                            prop.ParentId = guid;
                            prop.Id = _item.Id;

                            newObject.Add(prop);
                            recPropList.Add(prop);
                        }
                        else
                        {
                            if (reader.Depth > 0)
                            {
                                State = nameof(JArrayTree);
                                var _item = ProcessJsonData(reader, keyValuePairs, nameof(JObjectTree));
                                _item.ParentId = guid;
                                _item.Parent = newArray;


                                newArray.Add(_item);
                                recPropList.Add(_item);
                            }
                            else
                            {
                                State = nameof(JObjectTree);
                      

                            }

                        }

                            

                    }
                    else if (reader.TokenType == JsonToken.StartArray)
                    {

                        if (lastProp != "" && State == nameof(JObjectTree))
                        {
                            var _item = ProcessJsonData(reader, keyValuePairs, nameof(JArrayTree));
                            _item.ParentId = guid;
                            _item.Parent = newObject;

                            JPropertyTree prop = new JPropertyTree(lastProp, _item);

                            prop.Parent = newObject;
                            prop.ParentId = guid;
                            prop.Id = _item.Id;

                            newObject.Add(prop);
                            recPropList.Add(prop);

                         
                          
                        }
                        else
                        {
                            State = nameof(JArrayTree);
                              
                        }


                    }
                    else if (reader.TokenType == JsonToken.EndObject)
                    {
                        typeEnd = reader.TokenType;
                        var _ctx = new ObjectContext(guid, newObject, recPropList);
                        keyValuePairs.TryAdd(guid, _ctx);
                        _ctx.deep = reader.Depth;
                        return newObject;
                    }
                    else if (reader.TokenType == JsonToken.EndArray)
                    {
                        Guid _g = Guid.NewGuid();

                        newArray.Id = guid;
                   
                        var _ctx = new ObjectContext(guid, newArray, recPropList);

                        _ctx.deep = reader.Depth;
                        keyValuePairs.TryAdd(guid, _ctx);
                        return newArray;

                    }
                    else
                    {
                        throw new NotSupportedException();
                    }


                }


            }
            return State == nameof(JObjectTree) ? 
                (typeEnd != JsonToken.EndObject ? throw new ApplicationException() : newObject) : 
                    (typeEnd != JsonToken.EndArray ? throw new ApplicationException() : newArray);

        }

    }
}
