using Microsoft.AspNetCore.Mvc;

namespace SmartCharging.Api.Models.Requests;

public class UpdateChargeStationRequest
{
    public string Name { get; set; }
    public int GroupId { get; set; }
}