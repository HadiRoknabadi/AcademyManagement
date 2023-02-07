namespace AcademyManagement.Application.DTOs.PreRegisteration
{
    public class PreRegisteratinDetailsDTO
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Grade { get; set; }
         public bool WentToEnglishClassBefore { get; set; }
        public int? HowManyTermPassed { get; set; }
        public string BookNameReadInEnglishClass { get; set; }
        public string Description { get; set; }
    }
}