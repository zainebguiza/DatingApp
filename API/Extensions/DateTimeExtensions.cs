
namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalcuateAge(this DateOnly dob){
        var Today=DateOnly.FromDateTime(DateTime.UtcNow);
        var age=Today.Year-dob.Year;
        if(dob>Today.AddYears(-age)) age--;
         return age;
        }
    }
}