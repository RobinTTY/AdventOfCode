using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Task4
{
    static class Program
    {
        static void Main(string[] args)
        {
            var passports = new List<Passport>();

            // parse a single passport
            var parsedInput = File.ReadAllLines("input.txt").ToList();
            var itemBreakIndex = 0;

            while (itemBreakIndex != -1)
            {
                itemBreakIndex = parsedInput.FindIndex(match => match == "");
                var passport = CreatePassportSimple(parsedInput.TakeWhile(line => line != ""));

                if(passport != null)
                    passports.Add(passport);
                parsedInput.RemoveRange(0, itemBreakIndex + 1);
            }

            // allow passports where country id is null, ignore for other null values
            var result = passports.Where(passport => passport.GetType().GetProperties().Where(prop => prop !=
                typeof(Passport).GetProperty("CountryId")).ToList().TrueForAll(prop => prop.GetValue(passport) != null)).ToList();
            Console.WriteLine($"For part one there are {result.Count} valid passports.");
            Console.WriteLine($"For part two there are {result.Count(passport => passport.IsValid())} valid passports.");
            
        }

        private static Passport CreatePassportSimple(IEnumerable<string> passportString)
        {
            var passport = string.Join(",", string.Join(" ", passportString).Split(" ").Select(values => values.Split(":")
                .Select(str => $"\"{str}\"")).Select(kvp => string.Join(":", kvp)));
            passport = passport.PadLeft(passport.Length + 1, '{').PadRight(passport.Length + 2, '}');

            try
            {
                return JsonSerializer.Deserialize<Passport>(passport);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private static bool IsValid(this Passport passport)
        {
            var birthYearIsValid = Regex.IsMatch(passport.BirthYear, "^(19[2-9][0-9]|200[0-2])$");
            var issueYearValid = Regex.IsMatch(passport.IssueYear, "^(201[0-9]|2020)$");
            var expirationYearIsValid = Regex.IsMatch(passport.ExpirationYear, "^(202[0-9]|2030)$");
            var heightIsValid = Regex.IsMatch(passport.Height, "^(1([5-8][0-9]|9[0-3])cm|(59|6[0-9]|7[0-6])in)$");
            var hairColorIsValid = Regex.IsMatch(passport.HairColor, "^(#[a-f0-9]{6})$");
            var eyeColorIsValid = Regex.IsMatch(passport.EyeColor, "^(amb|blu|brn|gry|grn|hzl|oth)$");
            var passportIdIsValid = Regex.IsMatch(passport.Id, "^([0-9]{9})$");
            return birthYearIsValid && issueYearValid && expirationYearIsValid && heightIsValid && hairColorIsValid && eyeColorIsValid && passportIdIsValid;
        }
    }

    public class Passport
    {
        [JsonPropertyName("byr")]
        public string BirthYear { get; set; }
        [JsonPropertyName("iyr")]
        public string IssueYear { get; set; }
        [JsonPropertyName("eyr")]
        public string ExpirationYear { get; set; }
        [JsonPropertyName("hgt")]
        public string Height { get; set; }
        [JsonPropertyName("hcl")]
        public string HairColor { get; set; }
        [JsonPropertyName("ecl")]
        public string EyeColor { get; set; }
        [JsonPropertyName("pid")]
        public string Id { get; set; }
        [JsonPropertyName("cid")]
        public string CountryId { get; set; }
    }
}
