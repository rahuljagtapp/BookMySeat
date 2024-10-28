namespace BookMySeat.Domain.Models
{
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public string ResourceType { get; set; }

        public string ResourceName { get; set; }
       
        public bool? IsBooked { get; set; }


        public Resource(string resourceType,string resourceName)
        {
            ResourceId = Guid.NewGuid();
            ResourceType = resourceType;
            ResourceName = resourceName;
            IsBooked = null;
           
        }

    }
}
