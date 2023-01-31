namespace HVM_API.Dto
{
    public class UnitsDto
    {
        public string UnitCode {get;set;}
        public string UnitName {get;set;}
        public bool Active { get; set; }

        public List<UserUnitsDto> UserUnits { get; set; }
    }
}
