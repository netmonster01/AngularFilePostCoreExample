using System.Threading.Tasks;

namespace AngularFilePostCoreExample.Interfaces
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source_object);
        Task<TDestination> ConvertAsync(TSource source_object);

        TSource Convert(TDestination source_object);
        Task<TSource> ConvertAsync(TDestination source_object);
    }
}
