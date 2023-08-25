namespace FlexCoreService.UserCtrl.Dtos
{
    public class ProfileDto
    {
        public int? MemberId { get; set; }
        public int? fk_Level { get; set; }
        public string? LevelName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? CommonAddress { get; set; }
        public string? AlternateAddress1 { get; set; }
        public string? AlternateAddress2 { get; set; }
        public bool? IsSubscribeNews { get; set; }  
    }
}
