using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace ConsoleAppZet
{
    class Program
    {
        static void Main(string[] args)
        {
            Town list = new Town();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("https://xml.meteoservice.ru/export/gismeteo/point/148.xml");
            //xDoc.Load("D:\\Fun\\XMLFile1.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            //--------Town
            XmlNode vals = xRoot.SelectSingleNode("//MMWEATHER/REPORT/TOWN");
            list.Index = int.Parse(vals.SelectSingleNode("@index").Value);
            list.Sname = vals.SelectSingleNode("@sname").Value;
            list.Latitude = int.Parse(vals.SelectSingleNode("@latitude").Value);
            list.Longitude = int.Parse(vals.SelectSingleNode("@longitude").Value);
            //--------FORECAST
            XmlNodeList nodelist = vals.SelectNodes("*");
            int j = 0; //Счётчик
            foreach (XmlNode i in nodelist)
            {
                //-FORECAST-Attributes
                list.forecast[j].Day = int.Parse(i.SelectSingleNode("@day").Value);
                list.forecast[j].Month = int.Parse(i.SelectSingleNode("@month").Value);
                list.forecast[j].Year = int.Parse(i.SelectSingleNode("@year").Value);
                list.forecast[j].Hour = int.Parse(i.SelectSingleNode("@hour").Value);
                list.forecast[j].Tod = int.Parse(i.SelectSingleNode("@tod").Value);
                list.forecast[j].Predict = int.Parse(i.SelectSingleNode("@predict").Value);
                list.forecast[j].Weekday = int.Parse(i.SelectSingleNode("@weekday").Value);
                //--FORECAST-PHENOMENA-Attributes
                XmlNode xml = i.SelectSingleNode("PHENOMENA");

                list.forecast[j].phenomena.Cloudiness = int.Parse(xml.SelectSingleNode("@cloudiness").Value);
                list.forecast[j].phenomena.Precipitation = int.Parse(xml.SelectSingleNode("@precipitation").Value);
                list.forecast[j].phenomena.Rpower = int.Parse(xml.SelectSingleNode("@rpower").Value);
                list.forecast[j].phenomena.Spower = int.Parse(xml.SelectSingleNode("@spower").Value);
                //--FORECAST-PRESSURE-Attributes
                xml = i.SelectSingleNode("PRESSURE");
                list.forecast[j].pressure.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                list.forecast[j].pressure.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-TEMPERATURE-Attributes
                xml = i.SelectSingleNode("TEMPERATURE");
                list.forecast[j].temperature.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                list.forecast[j].temperature.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-WIND-Attributes
                xml = i.SelectSingleNode("WIND");
                list.forecast[j].wind.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                list.forecast[j].wind.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                list.forecast[j].wind.Direction = int.Parse(xml.SelectSingleNode("@direction").Value);
                //--FORECAST-RELWET-Attributes
                xml = i.SelectSingleNode("RELWET");
                list.forecast[j].relwet.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                list.forecast[j].relwet.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-HEAT-Attributes
                xml = i.SelectSingleNode("HEAT");
                list.forecast[j].heat.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                list.forecast[j].heat.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                j++;
            }
            //Console.WriteLine(xml.OuterXml);
        }

    }
}

