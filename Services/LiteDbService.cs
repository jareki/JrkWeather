using JrkWeather.Constants;
using JrkWeather.Models.DB;
using LiteDB;

namespace JrkWeather.Services
{
    public class LiteDbService
    {
        #region Fields

        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DefaultConstants.DbName);

        #endregion

        #region Private Methods

        private ResponseData? GetStoredDataCore(ILiteCollection<ResponseData> collection, Guid locationId)
        {
            collection.EnsureIndex(x => x.LocationId);
            var data = collection.FindOne(x => x.LocationId == locationId);
            return data;
        }

        #endregion

        #region Public Methods

        public ResponseData? GetStoredData(Guid locationId)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var dataCollection = db.GetCollection<ResponseData>();
                return GetStoredDataCore(dataCollection, locationId);
            }
        }

        public void StoreData(Guid locationId, string response)
        {
            using (var db = new LiteDatabase(DbPath))
            {
                var dataCollection = db.GetCollection<ResponseData>();
                var data = GetStoredDataCore(dataCollection, locationId);
                if (data != null)
                {
                    data.Data = response;
                    data.UpdateDateTime = DateTime.UtcNow;
                    dataCollection.Update(data);
                }
                else
                {
                    data = new ResponseData()
                    {
                        Data = response,
                        Id = ObjectId.NewObjectId(),
                        LocationId = locationId,
                        UpdateDateTime = DateTime.UtcNow
                    };
                    dataCollection.Insert(data);
                }
            }
        }

        #endregion
    }
}
