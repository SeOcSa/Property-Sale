using System;

namespace Data
{
    public class PropertyDetails
    {
        public Guid Id { get; set; }
        public String Type { get; set; }
        public int Rooms { get; set; }
        public bool Kitchen { get; set; }
        public int Bathroom { get; set; }
        public int  Surface { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public String urlPhoto { get; set; }
        public String urlPhoto2 { get; set; }
        public String urlPhoto3 { get; set; }
        public String urlPhoto4 { get; set; }
    }
}
