using System;

namespace BusinessEntities
{
    public class UserRegistrationModel
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Verificationcode { get; set; }
        public string EmailVerified { get; set; }

    }
}
