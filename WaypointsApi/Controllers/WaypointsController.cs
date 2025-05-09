using Microsoft.AspNetCore.Mvc;
using WaypointsApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WaypointsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaypointsController : ControllerBase
    {
        private readonly WaypointsDbContext _context;

        public WaypointsController(WaypointsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waypoint>>> GetAll()
        {
            return Ok(await _context.Waypoints.Include(w => w.Location).Include(w => w.BorderPoints).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Waypoint>> AddWaypoint([FromBody] Waypoint waypoint)
        {
            // Ensure the Id is not set by the client
            waypoint.Id = 0;
            if (waypoint.Location != null)
            {
                waypoint.Location.Id = 0;
                _context.Locations.Add(waypoint.Location);
                await _context.SaveChangesAsync();
                waypoint.LocationId = waypoint.Location.Id;
            }
            if (waypoint.BorderPoints != null)
            {
                foreach (var bp in waypoint.BorderPoints)
                {
                    bp.Id = 0;
                    // WaypointId will be set after saving the waypoint
                }
            }
            _context.Waypoints.Add(waypoint);
            await _context.SaveChangesAsync();
            if (waypoint.BorderPoints != null)
            {
                foreach (var bp in waypoint.BorderPoints)
                {
                    bp.WaypointId = waypoint.Id;
                }
                _context.BorderPoints.AddRange(waypoint.BorderPoints);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetAll), new { id = waypoint.Id }, waypoint);
        }

        [HttpPut("settings")]
        public async Task<ActionResult> UpdateSettings([FromBody] WaypointSettings settings)
        {
            var existing = await _context.WaypointSettings.FirstOrDefaultAsync();
            if (existing != null)
            {
                existing.DefaultEnterDistance = settings.DefaultEnterDistance;
                existing.DefaultExitDistance = settings.DefaultExitDistance;
                existing.DefaultMaxEnteringDeviationAngle = settings.DefaultMaxEnteringDeviationAngle;
            }
            else
            {
                _context.WaypointSettings.Add(settings);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waypoint>> GetById(int id)
        {
            var waypoint = await _context.Waypoints
                .Include(w => w.Location)
                .Include(w => w.BorderPoints)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (waypoint == null)
                return NotFound();
            return Ok(waypoint);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWaypoint(int id, [FromBody] Waypoint updated)
        {
            if (id != updated.Id)
                return BadRequest();
            var waypoint = await _context.Waypoints
                .Include(w => w.Location)
                .Include(w => w.BorderPoints)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (waypoint == null)
                return NotFound();
            // Update fields
            waypoint.Ref = updated.Ref;
            waypoint.Extra = updated.Extra;
            waypoint.Name = updated.Name;
            waypoint.Gate = updated.Gate;
            waypoint.Direction = updated.Direction;
            waypoint.EnterDistance = updated.EnterDistance;
            waypoint.ExitDistance = updated.ExitDistance;
            waypoint.Length = updated.Length;
            // Update location
            if (waypoint.Location != null && updated.Location != null)
            {
                waypoint.Location.Latitude = updated.Location.Latitude;
                waypoint.Location.Longitude = updated.Location.Longitude;
            }
            // Update border points
            if (updated.BorderPoints != null)
            {
                _context.BorderPoints.RemoveRange(waypoint.BorderPoints);
                waypoint.BorderPoints = updated.BorderPoints;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWaypoint(int id)
        {
            var waypoint = await _context.Waypoints
                .Include(w => w.Location)
                .Include(w => w.BorderPoints)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (waypoint == null)
                return NotFound();
            _context.Waypoints.Remove(waypoint);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Additional CRUD methods can be added here
    }
} 