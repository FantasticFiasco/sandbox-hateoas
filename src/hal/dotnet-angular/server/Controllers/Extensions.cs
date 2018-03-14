using System.Collections.Generic;
using AutoMapper;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    public static class Extensions
    {
        public static T MapTo<T>(this object self)
        {
            return Mapper.Map<T>(self);
        }

        public static HALResponse AddLink(this HALResponse self, Link link)
        {
            return self.AddLinks(link);
        }

        public static HALResponse AddEmbeddedCollection<T>(
            this HALResponse self,
            string collectionName,
            IEnumerable<T> models,
            Link link)
        {
            return self.AddEmbeddedCollection(collectionName, models, new[] { link });
        }

        public static HALResponse AddEmbeddedResource<T>(
            this HALResponse self,
            string resourceName,
            T model,
            Link link)
        {
            return self.AddEmbeddedResource(resourceName, model, new[] { link });
        }

        public static HALResponse AddLocationHeader(this HALResponse self, ControllerBase controller, int resourceId)
        {
            controller.Response.Headers.Add("Location", $"{controller.Request.Path}/{resourceId}");
            return self;
        }
    }
}
