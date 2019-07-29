using System.Threading.Tasks;

using PromoServiceCosmos.Models;

namespace PromoServiceCosmos.DataAccess
{
    public interface ICosmosDataAdapter
    {
        Task<bool> CreateDatabase(string name);
        Task<bool> CreateCollection(string dbName, string name);
        Task<bool> CreateDocument(string dbName, string name, ProductPromo productpromo);
        Task<bool> CreateAction(string dbName, string name, ProductPromoAction productpromoaction);
        Task<ProductPromo> DeleteUserAsync(string dbName, string name, string id);

        Task<dynamic> GetData(string dbName, string name);
        //Task<dynamic> GetData(string dbName, string name, ProductPromo productpromo);
        Task<bool> updateDocumentAsync(string dbName, string name, ProductPromo productpromo);
        Task<dynamic> GetDataById(string dbName, string name, string id);        
    }
}