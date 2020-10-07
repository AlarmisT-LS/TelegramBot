using System;
using System.Collections.Generic;
using System.Text;

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


        public Town()
        {
            Forecast temp;
            forecast = new List<Forecast>();
            for (int i = 0; i < size; i++)
            {
                temp = new Forecast();
                forecast.Add(temp);
            }

            for (int i = 0; i < size; i++)
            {
                forecast[i].phenomena = new Phenomena();
                forecast[i].pressure = new Pressure();
                forecast[i].temperature = new Temperature();
                forecast[i].wind = new Wind();
                forecast[i].relwet = new Relwet();
                forecast[i].heat = new Heat();
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
    class Temperature
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
    class Wind
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Direction { get; set; }
    }
    class Relwet
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
    class Heat
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
