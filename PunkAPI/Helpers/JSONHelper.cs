using Newtonsoft.Json;
using PunkAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PunkAPI.Helpers
{
    public class JSONHelper
    {
        public static string FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.json");

        public static void AddReview(InputValidation rating)
        {
            var jsonData = File.ReadAllText(FILE_PATH);

            var Ratings = JsonConvert.DeserializeObject<List<InputValidation>>(jsonData)
                                  ?? new List<InputValidation>();
            Ratings.Add(rating);

            jsonData = JsonConvert.SerializeObject(Ratings);
            File.WriteAllText(FILE_PATH, jsonData);
        }

        public static List<InputValidation> FetchAllReviews()
        {
            var jsonData = File.ReadAllText(FILE_PATH);

            var Ratings = JsonConvert.DeserializeObject<List<InputValidation>>(jsonData)
                                  ?? new List<InputValidation>();
            return Ratings;
        }
    }
}