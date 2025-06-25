namespace ConsoleTestApp
{
    class Pallet
    {
        public int ID { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public double weight { get; set; }
        public double size {  get; set; }
        public DateOnly? expirationDate { get; set; }
        public List<Box>? boxes { get; set; }
        public Pallet(int iD, double height, double width, double length, List<Box>? boxes)
        {
            ID = iD;
            this.height = height;
            this.width = width;
            this.length = length;
            this.weight = 30;
            this.size = height * width * length;
            this.boxes = boxes;
            if (boxes != null && boxes.Count > 0)
            {
                expirationDate = DateOnly.MaxValue;

                foreach (Box b in boxes)
                {
                    if (expirationDate.Value > b.expirationDate.Value)
                        this.expirationDate = b.expirationDate;
                    this.weight += b.weight;
                    this.size += b.size;
                }
            }
            else
            {
                this.expirationDate = null;
            }
        }
    }
}
