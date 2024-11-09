namespace PracticeWeb.Classes
{
	public class User
	{
		public int Id { get; set; }

		public string NickName { get; set; } = null!;

		public string Login { get; set; } = null!;

		public string Password { get; set; } = null!;

		public int RoleId { get; set; }
	}
}
