namespace Application.Common.Utils
{
    public static class Validators
    {
        public static bool IsShortStringValid(string str)
        {
            return IsStringValid(str) && str.Length <= 100;
        }
        
        public static bool IsLongStringValid(string str)
        {
            return IsStringValid(str) && str.Length <= 500;
        }
        
        public static bool IsIdValid(int id)
        {
            return id > 0;
        }
         
        public static bool IsTagsListToCreateValid(List<string> tagsList)
        {
            return tagsList.Count < 10;
        }

        private static bool IsStringValid(string str)
        {
            return str.Length > 0;
        }
    }
}
