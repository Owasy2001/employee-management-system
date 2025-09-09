namespace SAASShopDomain
{
    public class UserRestrictionDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool? RestrictedStatus { get; set; }
        public string StatusDesc { get { if (RestrictedStatus == true) return "ON"; else return "OFF"; } }
    }
}
