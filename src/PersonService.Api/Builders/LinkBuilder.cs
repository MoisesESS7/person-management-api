using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Api.Builders
{
    public sealed class LinkBuilder : ILinkBuilder
    {
        private readonly IUrlHelper _urlHelper;

        public LinkBuilder(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            var actionContext = actionContextAccessor.ActionContext
                ?? throw new ArgumentNullException(nameof(actionContextAccessor));

            _urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
        }

        public PaginationLinks Build(
            string actionName,
            int pageNumber,
            int pageSize,
            int totalPages)
        {
            var links = new PaginationLinks
            {
                Self = _urlHelper.Link(actionName, GetValues(pageNumber, pageSize)) ?? string.Empty
            };

            if (pageNumber > 1)
            {
                links.Prev = _urlHelper.Link(actionName, GetValues(pageNumber - 1, pageSize));
            }

            if (pageNumber > 2)
            {
                links.First = _urlHelper.Link(actionName, GetValues(1, pageSize));
            }

            if (pageNumber < totalPages)
            {
                links.Next = _urlHelper.Link(actionName, GetValues(pageNumber + 1, pageSize));
            }

            if (pageNumber < (totalPages - 1))
            {
                links.Last = _urlHelper.Link(actionName, GetValues(totalPages, pageSize));
            }

            return links;
        }

        private static RouteValueDictionary GetValues(int pageNumber, int pageSize)
        {
            return new RouteValueDictionary
            {
                ["page"] = pageNumber,
                ["page-size"] = pageSize
            };
        }
    }
}
