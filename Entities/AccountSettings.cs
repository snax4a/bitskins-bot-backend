using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Owned]
    [Table("AccountSettings")]
    public class AccountSettings
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public bool IsBitskinsEnabled { get; set; }
        public bool IsDmarketEnabled { get; set; }
        public bool IsAccountActive { get; set; }
        [JsonIgnore]
        public string DmarketAuthToken { get; set; }
    }
}
