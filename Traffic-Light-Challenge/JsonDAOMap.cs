using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Traffic_Light_Challenge
{
    /// <summary>
    /// Used to load the map from JSON file to Map Class
    /// Makes sure the map is valid, throws an MapNotValidException otherwise
    /// </summary>
    public class JsonDAOMap : DAOMap
    {
        //create an object of SingleObject
        private static JsonDAOMap instance = new JsonDAOMap();
        private string path;

        //make the constructor private so that this class cannot be
        //instantiated
        private JsonDAOMap()
        {
            path = Directory.GetCurrentDirectory() + "\\Maps\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        //Get the only object available
        public static JsonDAOMap getInstance()
        {
            return instance;
        }

        public Map LoadMap(uint mapID)
        {
            string filePath = path + mapID + ".json";
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                return parseJsonToMap(File.ReadAllText(filePath));
            }
        }

        public void SaveMap(Map map)
        {
            string json = parseMapToJson(map);
            File.WriteAllText(path + map.ID + "json", json);
        }

        private Map parseJsonToMap(string json)
        {
            uint id = 1;
            uint width = 3;
            uint height = 3;
            BaseField[,] baseField = new BaseField[width,height];
            bool agtl = true;

            Map map = new Map(id, width, height, baseField, agtl);
            return map;
        }

        //Fuer spaeter einen genormten Parser verwenden
        private string parseMapToJson(Map map)
        {
            string json = "{";
            json += "\"id\"=" + map.ID + ",";
            json += "\"width\"=" + map.Width + ",";
            json += "\"height\"=" + map.Height + ",";
            json += "[";
            for(int row = 0; row < map.Height; row++)
            {
                json += "[";
                for (int column=0; column < map.Width; column++)
                {
                    BaseField field = map.BaseField[row, column];
                    if(field is Street)
                    {
                        json += "\"street\"]";

                    }
                    else if(field is TrafficLight)
                    {
                        json += "\"trafficLight\"]";
                    }
                    else
                    {
                        json += "\"base\"]";
                    }
                    if(column<map.Width - 1)
                    {
                        json += ",";
                    }
                }
                if(row<map.Height - 1)
                {
                    json += ",";
                }
            }
            json += "]";
            json += "}";
            return json;
        }

        private void GetEverything(string jsonString, ref uint id, ref uint width, ref uint height, ref bool agtl, BaseField[,] map)
        {
            //Overall Start
            jsonString = removeSpaces(jsonString);

            if (jsonString[0] == '[')
            {
                jsonString.Trim(new char[2] { '[', ']' });
                jsonString = removeSpaces(jsonString);
                jsonString = jsonString.Substring(1,findIndexOfClosingBracket(jsonString) - 1);
                foreach(string x in jsonString.Split(',')) //does not work as intended, as there are multiple layers ([{...
                {
                    string parameter = x.Split(':')[0].Trim('\"');
                    string value = x.Split(':')[1].Trim('\"');

                    switch(parameter)
                    {
                        case "id":
                            id = uint.Parse(parameter);
                            break;
                        case "width":
                            width = uint.Parse(parameter);
                            break;
                        case "height":
                            height = uint.Parse(parameter);
                            break;
                        case "agtl":
                            agtl = (String.Equals(parameter,"true")) ? true : false;
                            break;
                        case "field":
                            break;

                    }
                }
            }

        }

        private string removeSpaces(string myString)
        {
            bool removeSpaces = true;
            string newString = "";
            foreach(char x in myString)
            {
                if (x == '"')
                {
                    removeSpaces = !removeSpaces;
                }
                if (!(" \t\r\n".IndexOf(x) == -1 && removeSpaces))
                    newString += x;
            }
            return newString;
        }

        private int findIndexOfClosingBracket(string myString)
        {
            int index = 0;
            uint noOfOpenBrackets = 0;
            uint noOfClosingBrackets = 0;
            do
            {
                switch (myString[index])
                {
                    case '{':
                        noOfOpenBrackets++;
                        break;
                    case '}':
                        noOfClosingBrackets++;
                        break;
                }
                index++;
            } while (noOfOpenBrackets == noOfClosingBrackets);
            return index - 1;
        }
    }
}
