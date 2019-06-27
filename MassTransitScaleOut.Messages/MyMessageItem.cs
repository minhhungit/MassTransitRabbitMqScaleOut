namespace MassTransitScaleOut.Messages
{
    public class MyMessageItem
    {
        public MyMessageItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
