using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentations.Controllers
{
    [ApiController]
    [Route("api")]
        public class RootController : ControllerBase 
        {
            private readonly LinkGenerator _linkGenerator;

            public RootController(LinkGenerator linkGenerator)
            {
                _linkGenerator = linkGenerator;
            }

            [HttpGet(Name = "GetRoot")]
            public async Task<ActionResult> GetRoot([FromHeader(Name = "Accept")]string mediaType)
            {
                if (mediaType.Contains("application/vnd.yusufkggoglu.apiroot"))
                {
                    var list = new List<Link>()
                {
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new{}),
                        Rel="_self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.GetAllBooks), new{}),
                        Rel="books",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.CreateOneBook), new{}),
                        Rel="books",
                        Method = "POST"
                    },
                };
                    return Ok(list);
                }
                return NoContent(); // 204
            }
        }

    }
