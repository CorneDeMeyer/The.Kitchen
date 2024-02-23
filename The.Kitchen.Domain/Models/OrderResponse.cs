namespace The.Kitchen.Domain.Models
{
    public class OrderResponse
    {
        /// <summary>
        /// Orders
        /// String - Receipe Name
        /// Int - Amount of people they can feed
        /// </summary>
        public Dictionary<string, int> Orders { get; set; }

        /// <summary>
        /// Sum of Order/s feeding
        /// </summary>
        public int Feeds => Orders?.Sum(o => o.Value) ?? 0;

        /// <summary>
        /// Reference to original ingrediants specified / request
        /// </summary>
        public required Dictionary<string, int> OriginalIngrediants { get; set; }
        
        /// <summary>
        /// If any left over ingrediants are present.
        /// </summary>
        public Dictionary<string, int> LeftOverIngrediants { get; set; }

        /// <summary>
        /// If any errors were to apply
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Constructor to initiate Error, Orders and LeftOverIngrediants List
        /// </summary>
        public OrderResponse()
        {
            LeftOverIngrediants = new Dictionary<string, int>();
            Orders = new Dictionary<string, int>();
            Errors = new List<string>();
        }
    }
}
