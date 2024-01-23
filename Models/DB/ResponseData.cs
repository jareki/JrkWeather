using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace JrkWeather.Models.DB
{
    public class ResponseData
    {
        public ObjectId Id { get; set; }

        public Guid LocationId { get; set; }

        public string? Data { get; set; }

        public DateTime UpdateDateTime { get; set; }
        

        public ResponseData()
        {
            this.Id = ObjectId.NewObjectId();
        }
    }
}
