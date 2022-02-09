namespace GPPlanetGQL.GraphQL
{
    public class Unions
    {
        [UnionType("Notification")]
        public interface INotification
        {
            public int ToId { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
