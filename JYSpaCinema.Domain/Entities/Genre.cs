namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 电影分类
    /// </summary>
    public class Genre : IEntityBase<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
