using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Traffic_Light_Challenge
{
    class JsonDAOMap : DAOMap
    {
        private string path;

        public JsonDAOMap()
        {
            path = Directory.GetCurrentDirectory() + "\\Maps\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
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
            uint id = Convert.ToUInt32(json.Split(',')[0].Split('=')[1]);
            uint width = Convert.ToUInt32(json.Split(',')[1].Split('=')[1]);
            uint height = Convert.ToUInt32(json.Split(',')[2].Split('=')[1]);
            BaseField[,] baseField = new BaseField[height, width];
            for(int row = 0; row < height; row++)
            {
                string fieldRow = json.Split('[')[2 + row].Split(']')[0];
                for (int column = 0; column < width; column++)
                {
                    switch(fieldRow.Split(',')[column])
                    {
                        case "\"base\"":
                            baseField[row, column] = new BaseField();
                            break;
                        case "\"street\"":
                            baseField[row, column] = new Street();
                            break;
                        case "\"trafficLight\"":
                            baseField[row, column] = new TrafficLight();
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            Map map = new Map(id, width, height, baseField);
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
    }
}
