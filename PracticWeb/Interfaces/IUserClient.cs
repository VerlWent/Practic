using PracticeWeb.Classes;
using PracticeWeb.Classes.LocalClasses;
using Refit;

namespace PracticeWeb.Interfaces
{
    public interface IUserClient
    {
        [Get("/Product")]
        Task<List<Product>> GetProduct();

		//[Get("/Authorization")]
		//Task<string> GetUsers([Query] string login, [Query] string password);

		[Get("/Authorization")]
		Task<UserTokenResponse> GetUsers([Query] string login, [Query] string password);

		[Get("/User/GetCart")]
        Task<List<OrderClass>> GetOrder([Header("Authorization")] string token);

        [Post("/Registration")]
        Task CreateUser([Body] User user);

        //[Post("/User/BuyFullCart")]
        //Task<HttpResponseMessage> BuyFullCart([Header("Authorization")] string token, List<int> order);

        [Post("/User/BuyFullCart")]
        Task<HttpResponseMessage> BuyFullCart([Header("Authorization")] string token, List<NewOrderClass> order);



        [Get("/SystemAdministrator/GetAllUsers")]
        Task<List<UserForSystemClass>> GetAllUser([Header("Authorization")] string token);

        [Get("/SystemAdministrator/GetOneUser")]
        Task<List<UserForSystemClass>> GetOneUser([Header("Authorization")] string token, int Id);

        [Put("/SystemAdministrator/EditUser")]
        Task<HttpResponseMessage> EditUser([Header("Authorization")] string token, int Id, string NickName, string Login, string Password, string Role);

        [Post("/SystemAdministrator/CreateUser")]
        Task<HttpResponseMessage> CreateUser([Header("Authorization")] string token, string NickName, string Login, string Password, string Role);

        [Delete("/SystemAdministrator/DeleteUser")]
        Task<HttpResponseMessage> DeleteUser([Header("Authorization")] string token, int Id);




        [Get("/StoreAdministrator/GetAllProduct")]
        Task<List<Product>> GetAllProduct([Header("Authorization")] string token);

        [Put("/StoreAdministrator/EditProduct")]
        Task<HttpResponseMessage> EditProduct([Header("Authorization")] string token, int Id, string Name, string Description, string Image, double Price, String Category, int CountInStock);

        [Post("/StoreAdministrator/CreateProduct")]
        Task<HttpResponseMessage> CreateProduct([Header("Authorization")] string token, string Name, string Description, string Image, double Price, String Category, int CountInStock);

        [Delete("/StoreAdministrator/DeleteProduct")]
        Task<HttpResponseMessage> DeleteProduct([Header("Authorization")] string token, int Id);
    }
}
