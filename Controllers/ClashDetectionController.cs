using Microsoft.AspNetCore.Mvc;

using ClashDetectionServer.Interfaces;
using ClashDetectionServer.Contracts.Responses;
using ClashDetectionServer.Contracts.Requests;
using ClashDetectionServer.Managers;
using ClashDetectionServer.Models;
namespace ClashDetectionServer.Controllers;

[ApiController]
[Route("api/v1/clashes")]
public class ClashDetectionController(IClashDetectionManager clashDetectionManager) : ControllerBase
{
    [HttpPost("detect")]
    public ActionResult<DetectClashesResponse> Detect([FromBody] DetectClashesRequest request)
    {
        var sitePlan = new SitePlan(request.SitePlan.Width, request.SitePlan.Length);
        var buildings = request.Buildings.Select(b => new Building(
                                                            b.Name!,
                                                            Enum.Parse<BuildingType>(b.Type, true),
                                                            b.X,
                                                            b.Y,
                                                            b.Length,
                                                            b.Width)).ToList();
       
        var clashes = clashDetectionManager.Detect(sitePlan, buildings);
        return Ok(clashes);
    }



}
