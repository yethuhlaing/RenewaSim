using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
public class FetchWeatherData : MonoBehaviour
{
    public InputField countryInput;
    // public Text TemperatureText;
    // public Text PressureText;
    // public Text HumidityText;
    // public Text DescriptionText;
    // public Text WindSpeedText;
    // public Text WindDegreeText;
    public Text GeneralText; // The prefab of the Text element you want to instantiate
                            // Parent transform where the Text elements will be instantiated

    private RectTransform inputFieldTransform; 
 
    private const string API_KEY = "903f6c812d46a271d92b9e4cb94d52f1";
    private const string GEO_API_URL = "https://api.openweathermap.org/geo/1.0/direct";
    private const string WEATHER_API_URL = "https://api.openweathermap.org/data/2.5/forecast";
    private const string AIR_POLLUTION_URL = "https://api.openweathermap.org/data/2.5/air_pollution";
    private string metric = "metric";
    public float verticalSpacing = 100f;
    public delegate void WeatherUpdateEvent(WeatherItem newData);
    public static event WeatherUpdateEvent OnWeatherUpdate;

    private void Start()
    {
        GameObject.Find("FetchButton").GetComponent<Button>().onClick.AddListener(GetWeatherData); 
        GameObject.Find("FetchPollutionButton").GetComponent<Button>().onClick.AddListener(GetPollutionData);
        inputFieldTransform = GameObject.Find("Canvas").GetComponentInChildren<Button>().GetComponent<RectTransform>();

    }
    public void AddWeatherData(WeatherItem item)
    {
        if (OnWeatherUpdate != null) 
        {
            OnWeatherUpdate(item);
        }
    }
    void GetWeatherData() => StartCoroutine(GetCityCoordinates(countryInput.text, WEATHER_API_URL));
    void GetPollutionData() => StartCoroutine(GetCityCoordinates(countryInput.text, AIR_POLLUTION_URL));

    private IEnumerator GetCityCoordinates(string cityName, string API_URL)
    {
        

        string geoRequestUrl = $"{GEO_API_URL}?q={cityName}&limit=1&appid={API_KEY}";

        using (UnityWebRequest request = UnityWebRequest.Get(geoRequestUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                yield break;
            }

            string response = request.downloadHandler.text;
            GeoData[] data = JsonHelper.FromJson<GeoData>(response);

            if (data.Length == 0)
            {
                Debug.LogWarning($"No coordinates found for {cityName}");
                yield break;
            }

            double lat = data[0].lat;
            double lon = data[0].lon;
            StartCoroutine(GetWeatherDetails(lat, lon, API_URL));
        }
    }
    private IEnumerator GetWeatherDetails(double lat, double lon, string API_URL)
    {
        string weatherRequestUrl = $"{API_URL}?lat={lat}&lon={lon}&appid={API_KEY}&units={metric}";
        using (UnityWebRequest request = UnityWebRequest.Get(weatherRequestUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                yield break;
            }

            string response = request.downloadHandler.text;
            // Process weather details as needed
            Debug.Log(response);
            if (API_URL.Contains("air"))
            {
                UpdatePollutionData(response);
            }
            else
            {
                UpdateWeatherData(response);    
            }
        }
    }

    [Serializable]
    private class GeoData
    {
        public string name;
        public double lat;
        public double lon;
    }
    [Serializable]
    public class WeatherData
    {
        public string cod;
        public int message;
        public int cnt;
        public List<WeatherItem> list;
    }
    [Serializable]
    public class PollutionData
    {
        public List<PollutionItem> list;
    }
    [Serializable]
    public class WeatherItem
    {
        public MainInfo main;
        public List<WeatherInfo> weather;
        public WindInfo wind;
        public int dt;
        public int timezone;
        public sysInfo sys;
        public PrecipationInfo precipation;
    }
    [Serializable]
    public class PollutionItem
    {
        public GasInfo components;

    }
    [Serializable]
    public class MainInfo
    {
        public float temp;
        public float pressure;
        public float humidity;
        // Add other properties as needed
    }

    [Serializable]
    public class WeatherInfo
    {
        public int id;
        public string main;
        public string description;
        // Add other properties as needed
    }
    [Serializable]
    public class WindInfo
    {
        public float speed;
        public float deg;

    }
    [Serializable]
    public class PrecipationInfo
    {
        public int pop;
    }
    [Serializable]
    public class sysInfo
    {
        public int sunriseTime;
        public int sunsetTime;
    }
    [Serializable]
    public class GasInfo
    {
        public float co;
        public float no;
        public float no2;
        public float o3;
        public float so2;
        public float pm2_5;
        public float pm10;
        public float nh3;
    }
    private static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"array\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }

    void UpdateWeatherData(string jsonString)
    {

        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonString);
        
        if (weatherData != null)
        {
            // Extract only current time
            WeatherItem item = weatherData.list[0];
            GeneralText.text = "Description: " + item.weather[0].description + "\n" + 
                                    "Temperature: " + item.main.temp + " Celsius" + "\n" +
                                    "Pressure: " + item.main.pressure + " hPa" + "\n" +
                                    "Humidity: " + item.main.humidity + " %" + "\n" +
                                    "Wind Speed: " + item.wind.speed + " meter/sec" + "\n"  +
                                    "Wind Direction: " + item.wind.deg + " Degree";
            Debug.Log(item.precipation.pop);
            AddWeatherData(item);
        }
    }
    void UpdatePollutionData(string jsonString)
    {

        PollutionData pollutionData = JsonUtility.FromJson<PollutionData>(jsonString);

        if (pollutionData != null)
        {
            // Extract only current time
            PollutionItem item = pollutionData.list[0];
            GeneralText.text = "CO: " + item.components.co + " μg/m3" + "\n" +
                                    "NO: " + item.components.no + " μg/m3" + "\n" +
                                    "NO2: " + item.components.no2 + " μg/m3" + "\n" +
                                    "O3: " + item.components.o3 +" μg/m3"+ "\n" +
                                    "SO2: " + item.components.so2 + " μg/m3" + " μg/m3" + "\n" +
                                    "PM2_5: " + item.components.pm2_5 + " μg/m3" + "\n" +
                                    "PM10: " + item.components.pm10 + " μg/m3" + "\n"+
                                    "NH3: " + item.components.nh3 + " μg/m3";
            Debug.Log(item.components);
            Debug.Log(item.components.co);

        }
    }
}
