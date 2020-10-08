using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ConsoleAppZet
{
    class Town // информация о пункте прогнозирования
    {
        public int Index { get; set; } // уникальный код города
        public string Sname { get; set; } //закодированное название города
        public int Latitude { get; set; } //широта в целых градусах
        public int Longitude { get; set; } //долгота в целых градусах
        public List<Forecast> Forecast;

        public Town()
        {
            Forecast = new List<Forecast>();
            XmlGismetio();
        }
        public void XmlGismetio()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("https://xml.meteoservice.ru/export/gismeteo/point/148.xml");
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
                //Создаём элемент Forecast
                Forecast temp = new Forecast();//--
                Forecast.Add(temp);//--
                //-FORECAST-Attributes
                Forecast[j].Day = int.Parse(i.SelectSingleNode("@day").Value);
                Forecast[j].Month = int.Parse(i.SelectSingleNode("@month").Value);
                Forecast[j].Year = int.Parse(i.SelectSingleNode("@year").Value);
                Forecast[j].Hour = int.Parse(i.SelectSingleNode("@hour").Value);
                Forecast[j].Tod = int.Parse(i.SelectSingleNode("@tod").Value);
                Forecast[j].Predict = int.Parse(i.SelectSingleNode("@predict").Value);
                Forecast[j].Weekday = int.Parse(i.SelectSingleNode("@weekday").Value);
                //--FORECAST-PHENOMENA-Attributes
                XmlNode xml = i.SelectSingleNode("PHENOMENA");

                Forecast[j].phenomena.Cloudiness = int.Parse(xml.SelectSingleNode("@cloudiness").Value);
                Forecast[j].phenomena.Precipitation = int.Parse(xml.SelectSingleNode("@precipitation").Value);
                Forecast[j].phenomena.Rpower = int.Parse(xml.SelectSingleNode("@rpower").Value);
                Forecast[j].phenomena.Spower = int.Parse(xml.SelectSingleNode("@spower").Value);
                //--FORECAST-PRESSURE-Attributes
                xml = i.SelectSingleNode("PRESSURE");
                Forecast[j].pressure.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                Forecast[j].pressure.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-TEMPERATURE-Attributes
                xml = i.SelectSingleNode("TEMPERATURE");
                Forecast[j].temperature.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                Forecast[j].temperature.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-WIND-Attributes
                xml = i.SelectSingleNode("WIND");
                Forecast[j].wind.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                Forecast[j].wind.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                Forecast[j].wind.Direction = int.Parse(xml.SelectSingleNode("@direction").Value);
                //--FORECAST-RELWET-Attributes
                xml = i.SelectSingleNode("RELWET");
                Forecast[j].relwet.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                Forecast[j].relwet.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                //--FORECAST-HEAT-Attributes
                xml = i.SelectSingleNode("HEAT");
                Forecast[j].heat.Max = int.Parse(xml.SelectSingleNode("@max").Value);
                Forecast[j].heat.Min = int.Parse(xml.SelectSingleNode("@min").Value);
                j++;
            }
        }
    }
    class Forecast //информация о сроке прогнозирования
    {
        public int Day { get; set; } //дата на которую составлен прогноз в данном блоке
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; } //местное время, на которое составлен прогноз
        public int Tod { get; set; }//время суток, для которого составлен прогноз: 0 - ночь 1 - утро, 2 - день, 3 - вечер
        public int Predict { get; set; } //день недели, 1 - воскресенье, 2 - понедельник, и т.д.
        public int Weekday { get; set; }// заблаговременность прогноза в часах

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
    class Phenomena //атмосферные явления
    {
        //облачность по градациям: -1 - туман, 0 - ясно, 1 - малооблачно, 2 - облачно, 3 - пасмурно
        public int Cloudiness { get; set; }
        //precipitation - тип осадков: 3 - смешанные, 4 - дождь, 5 - ливень, 6,7 – снег, 8 - гроза, 9 - нет данных, 10 - без осадков
        public int Precipitation { get; set; }
        //интенсивность осадков, если они есть. 0 - возможен дождь/снег, 1 - дождь/снег
        public int Rpower { get; set; }
        //вероятность грозы, если прогнозируется: 0 - возможна гроза, 1 - гроза
        public int Spower { get; set; }
    }
    class Pressure // атмосферное давление, в мм.рт.ст.
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
    class Temperature : Pressure { }//  температура воздуха, в градусах Цельсия
    class Wind : Pressure //приземный ветер
    {
        //min, max - минимальное и максимальное значения средней скорости ветра, без порывов(м/с)
        //direction - направление ветра в румбах, 0 - северный, 1 - северо-восточный, и т.д.
        public int Direction { get; set; }
    }
    class Relwet : Pressure { } //относительная влажность воздуха, в %

    //комфорт - температура воздуха по ощущению одетого по сезону человека, выходящего на улицу
    class Heat : Pressure { } 


}
