namespace MembershipSystem.Strategy.Models
{
    public class Product
    {
        [BsonId] //MongoDb
        [Key] // Efcore
        [BsonRepresentation(BsonType.ObjectId)] //MongoDb
        public string Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
