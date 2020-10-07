using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ConsoleAppZet
{
    class Town
    {
        public int Index { get; set; }
        public string Sname { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        private const int size = 4;
        public List<Forecast> forecast;
        Forecast temp;

        public Town()
        {
            forecast = new List<Forecast>();
            for (int i = 0; i < size; i++)
            {
                temp = new Forecast();
                forecast.Add(temp);
            }
            XmlGismetio();
        }
        public void XmlGismetio()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("https://xml.meteoservice.ru/export/gismeteo/point/148.xml");
            //xDoc.Load("D:\\Fun\\XMLFile1.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            //--------Town
            XmlNode vals = xRoot.SelectSingleNode("//MMWEATHER/REPORT/TOWN");
            Index = int.Parse(vals.SelectSingleNode("@index").Value);
            Sname = vals.SelectSingleNode("@sname").Value;
            Latitude = int.Parse(vals.SelectSingleNode("@latitude").Value);
            Longitude = int.Parse(vals.SelectSingleNode("@longitude").Value);
            //--------FORECAST
            XmlNodeList nodelist = vals.SelectNodes("*");
            int j = 0; //Счётчик
            foreach (XmlNode i in nodelist)
            {
                //-FORECAST-Attributes
                forecast[j].Day = int.Parse(i.SelectSingleNode("@day").Value);
                forecast[j].Month = int.Parse(i.SelectSingleNode("@month").Value);
                forecast[j].Year = int.Parse(i.SelectSingleNode("@year").Value);
                forecast[j].Hour = int.Parse(i.SelectSingleNode("@hour").Value);
                forecast[j].Tod = int.Parse(i.SelectSingleNode("@tod").Value);
                forecast[j].Predict = int.Parse(i.SelectSingleNode("@predict").Value);
                forecast[j].Weekday = int.Parse(i.SelectSingleNode("@weekday").Value);
                //--FORECAST-PHENOMENA-Attributes
                XmlNode xml = i.SelectSingleNode("PHENOMENA");

                forecast[j].phenomena.Cloudiness = int.Parse(xml.SelectSingleNode("@cloudiness").Value);
                forecast[j].phenomena.Precipitation = int.Parse(xml.SelectSingleNode("@precipitation").Value);
                forecast[j].phenomena.Rpower = int.Parse(xml.SelectSingleNode("@rpower").Value);
                forecast[j].phenomena.Spower = int.Parse(xml.SelectSingleNode("@spower").Value);
                //--FORECAST-PRESSURE-Attributes
                xml = i.SelectSingleNode("PRESSURE");
                forecast[j].pressure.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                forecast[j].pressure.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-TEMPERATURE-Attributes
                xml = i.SelectSingleNode("TEMPERATURE");
                forecast[j].temperature.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                forecast[j].temperature.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-WIND-Attributes
                xml = i.SelectSingleNode("WIND");
                forecast[j].wind.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                forecast[j].wind.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                forecast[j].wind.Direction = int.Parse(xml.SelectSingleNode("@direction").Value);
                //--FORECAST-RELWET-Attributes
                xml = i.SelectSingleNode("RELWET");
                forecast[j].relwet.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                forecast[j].relwet.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-HEAT-Attributes
                xml = i.SelectSingleNode("HEAT");
                forecast[j].heat.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                forecast[j].heat.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                j++;
            }
        }
    }

    class Forecast
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Tod { get; set; }
        public int Predict { get; set; }
        public int Weekday { get; set; }

        public Phenomena phenomena;
        public Pressure pressure;
        public Temperature temperature;
        public Wind wind;
        public Relwet relwet;
        public Heat heat;
        public Forecast()
        {
            phenomena = new Phenomena();
            pressure = new Pressure();
            temperature = new Temperature();
            wind = new Wind();
            relwet = new Relwet();
            heat = new Heat();
        }

    }
    class Phenomena
    {
        public int Cloudiness { get; set; }
        public int Precipitation { get; set; }
        public int Rpower { get; set; }
        public int Spower { get; set; }
    }
    class Pressure
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
    class Temperature : Pressure { }
    class Wind : Pressure
    {
        public int Direction { get; set; }
    }
    class Relwet : Pressure { }
    class Heat : Pressure { }


}
