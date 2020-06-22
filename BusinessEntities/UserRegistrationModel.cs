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
        //new field
        public int Id { get; set; }
        public string UserCode { get; set; }       
        public string UserNTID { get; set; }
        public string ManagerCode { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNTID { get; set; }
        public int RoleId { get; set; }
        public int LOBId { get; set; }
        public int Status { get; set; }
        public string StatusType { get; set; }
        public string RoleName { get; set; }
        public string LOBName { get; set; }
        public string BusinessSegmentName { get; set; }
        public string CapabilitiesName { get; set; }
        public string AboutMe { get; set; }
        public int CapabilitiesId { get; set; }
        public int BusinessSegmentId { get; set; }
        public byte[] UserPhoto { get; set; }
        public int ImgStatus { get; set; }
        public bool IsAdmin { get; set; }
        public string Remarks { get; set; }
        public string image { get; set; }


    }
}
