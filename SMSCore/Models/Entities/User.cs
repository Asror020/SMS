using SMSCore.Models.Common;

namespace SMSCore.Models.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// User email address
        /// </summary>
        public string EmailAddress { get; set; } = null!;

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; } = null!;

        /// <summary>
        /// Represents whether user email is verified
        /// </summary>
        public bool IsEmailVerified { get; set; }   

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryDate { get; set; }

        /// <summary>
        /// User university
        /// </summary>
        public University? University { get; set; }

        public bool IsUserConfirmed { get; set; } 
    }
}
