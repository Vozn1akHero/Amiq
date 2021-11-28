using Amiq.Contracts.Activity;
using Amiq.DataAccessLayer.Activity;
using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amiq.WebApi.Controllers
{
    public class ActivityController : AmiqBaseController
    {
        private DaoActivity _daActivity = new DaoActivity();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DtoCreateActivityInBulk dtoCreateActivityInBulk)
        {
            await _daActivity.CreateAsync(JwtStoredUserInfo.UserId, dtoCreateActivityInBulk);
            return Ok();
        }
    }
}
