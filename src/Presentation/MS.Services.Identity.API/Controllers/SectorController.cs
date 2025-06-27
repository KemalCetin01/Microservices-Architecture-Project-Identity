using Microsoft.AspNetCore.Mvc;
using MS.Services.Core.Base.Api;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.Sectors.Commands;
using MS.Services.Identity.Application.Handlers.Sectors.Queries;
using System.Globalization;

namespace MS.Services.Identity.API.Controllers;
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Sectors")]
[ApiController]
public class SectorController : BaseApiController
{
    private readonly IRequestBus _requestBus;
    public SectorController(IRequestBus requestBus) => _requestBus = requestBus;

    /// <summary>
    /// returns sector fkeylist with labels and values
    /// </summary>
    [HttpGet("fkey-list")]
    public async Task<IActionResult> GetAllSectors() =>
    Ok((await _requestBus.Send(new GetSectorsQuery() { })).Data);

    /// <summary>
    /// returns details
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _requestBus.Send(new GetSectorDetailsQuery() { Id = id }));
    }
    /// <summary>
    /// returns all sectors
    /// </summary>
    [HttpPost("search")]
    public async Task<IActionResult> Get([FromBody] SearchSectorsQuery searchSectorsQuery)
    => Ok(await _requestBus.Send(searchSectorsQuery));



    /// <remarks>
    /// Note: Name must be unique. 
    /// 
    ///  
    ///     POST /Sectors
    ///     {
    ///     "name": "test",
    ///     "status": 1
    ///     }
    ///     
    /// </remarks>
    /// <summary>
    /// creates sector
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSectorCommand createSectorCommand) =>
             Ok(await _requestBus.Send(createSectorCommand));

    /// <remarks>
    /// update sector
    /// 
    ///     PUT /Sectors
    ///       {
    ///         "id": "eabec798-136e-44bf-8a8f-3d5838e89e64",
    ///         "name": "test",
    ///         "status": 1
    ///       }
    /// </remarks>
    /// <summary>
    /// update sector
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSectorCommand updateSectorCommand)
       => Ok(await _requestBus.Send(updateSectorCommand));

    /// <summary>
    /// delete sector by id
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _requestBus.Send(new DeleteSectorCommand() { Id = id });
        return NoContent();
    }
}
