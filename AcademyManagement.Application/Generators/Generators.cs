namespace AcademyManagement.Application.Generators
{
    public class Generator
    {
        public static string GenerateActiveCodeForPhone()
        {
            return new Random().Next(10000, 99999).ToString();
        }

         public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GeneratePassword()
        {
            return GenerateUniqCode().Substring(0,9);
        }
    }
}