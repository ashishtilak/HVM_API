namespace HVM_API.Dto
{
    public class UserUnitsDto
    {
        public string UserName { get; set; }
        public UserDto User { get; set; }

        public string UnitCode {get;set;}
        public UnitsDto Unit { get; set; }

        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
