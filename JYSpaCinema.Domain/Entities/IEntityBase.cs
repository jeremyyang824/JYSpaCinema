namespace JYSpaCinema.Domain.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntityBase<TKey>
        where TKey : struct
    {
        TKey ID { get; set; }
    }
}
