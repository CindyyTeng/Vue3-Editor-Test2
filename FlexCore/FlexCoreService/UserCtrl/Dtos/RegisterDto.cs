namespace FlexCoreService.UserCtrl.Dtos
{
    public class RegisterDto
    {
        public string? Account { get; set; }

        public string? EncryptedPassword { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Mobile { get; set; }

        public string? CommonAddress { get; set; }

        public int? fk_LevelId { get; set; }
    }
}
