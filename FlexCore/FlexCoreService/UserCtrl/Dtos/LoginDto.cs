namespace FlexCoreService.UserCtrl.Dtos
{
    public class LoginDto
    {
        public string? Account { get; set; }

        public string? EncryptedPassword { get; set; }
    }
}
