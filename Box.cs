namespace ConsoleTestApp
{
    class Box
    {
        public int ID { get; set; }
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public double weight { get; set; }
        public double size { get; set; }
        public DateOnly? expirationDate { get; set; }
        public DateOnly? productionDate { get; set; }

        public Box(int iD, double height, double width, double length, double weight, DateOnly? expirationDate, DateOnly? productionDate)
        {
            ID = iD;
            this.height = height;
            this.width = width;
            this.length = length;
            this.weight = weight;
            this.size = width * length * height;
            if (!expirationDate.HasValue && !productionDate.HasValue)
                throw new Exception("There shoud be at least one date");

            if (productionDate.HasValue)
            {
                this.productionDate = productionDate;
                this.expirationDate = productionDate.Value.AddDays(100);
            }
            else
            {
                this.productionDate = null;
                this.expirationDate = expirationDate;
            }
        }
    }
}
