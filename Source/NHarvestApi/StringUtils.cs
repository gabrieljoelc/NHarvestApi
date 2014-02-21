using System.Text.RegularExpressions;

namespace NHarvestApi
{
    static class StringUtils
    {   
        // https://github.com/srkirkland/Inflector/blob/master/Inflector/Inflector.cs
        public static string Underscore(this string pascalCasedWord)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])",
                    "$1_$2"), @"[-\s]", "_").ToLower();
        }
    }
}
