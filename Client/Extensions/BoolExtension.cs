namespace WheelOfFortune.Client.Extensions
{
    public static class BoolExtension
    {
        public static string ToRus(this bool thisBool)
        {
            return thisBool ? "Да" : "Нет";
        }
    }
}
