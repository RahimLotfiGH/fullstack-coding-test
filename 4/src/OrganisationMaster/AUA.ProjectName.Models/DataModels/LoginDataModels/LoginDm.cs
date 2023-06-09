using AUA.ProjectName.Common.Enums;

namespace AUA.ProjectName.Models.DataModels.LoginDataModels
{
    public class LoginDm
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public int[]? RoleIds { get; set; }
        public int[]? UserAccessIds { get; set; }

        public EResultStatus ResultStatus { get; set; }

        public DateTime ExpiresIn { get; set; }

    }
}
