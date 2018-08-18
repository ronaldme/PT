using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace PT.Web.Infrastructure
{
    public static class Helpers
    {
        public static async Task<IPagedList<TDestination>> ProjectToPagedListAsync<TDestination>(
            this IQueryable queryable, IConfigurationProvider config, int pageNumber, int pageSize)
        {
            return await queryable.ProjectTo<TDestination>(config).ToPagedListAsync(pageNumber, pageSize);
        }

        public static SelectList ToSelectList(this Dictionary<int, string> dictionary)
        {
            return new SelectList(dictionary, "Key", "Value");
        }
    }
}