namespace SAASShopDomain
{
    public class AppUsersDTOBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppUserRoleDesc { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FullNameFormatted
        {
            get
            {
                var name = FullName;
                var arrayOfSymbols = new string[] { "/", "\\", "<", ">", "{", "}", "|", ")", "(", "*", "&", "^", "%", "$", "'#'", "!", "@", "[", "]", ":", "]" };
                for (int i = 0; i < arrayOfSymbols.Length; i++)
                    if (name.Contains(arrayOfSymbols[i]))
                    {
                        name = name.Replace(arrayOfSymbols[i], " ");
                    }
                return name;
            }
        }
    }

    public class AppUserDTO : AppUsersDTOBase
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime LastActiveDate { get; set; }

        public string Role { get; set; }

        public bool IsTeamMember { get; set; }

        public string LoginId { get; set; }

        public string IsTeamMemberFormatted
        {
            get
            {
                if (IsTeamMember == true)
                    return "Active";
                else return "Not Active";
            }
        }
    }

    public class AppUsersListDTO : AppUsersDTOBase
    {
    }
}